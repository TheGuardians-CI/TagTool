﻿using TagTool.Cache;
using TagTool.Commands.Editing;
using TagTool.Commands.Common;
using TagTool.Commands.Definitions;
using TagTool.Commands.Files;
using TagTool.Commands.Strings;
using TagTool.Commands.Sounds;
using TagTool.Commands.Porting;
using TagTool.Commands.Modding;
using TagTool.Commands.Bitmaps;
using TagTool.Commands.PhysicsModels;
using TagTool.Commands.CollisionModels;
using TagTool.Commands.Shaders;
using TagTool.Commands.GUI;
using TagTool.Commands.HUD;
using TagTool.Cache.HaloOnline;

namespace TagTool.Commands.Tags
{
    public static class TagCacheContextFactory
    {
        public static CommandContext Create(CommandContextStack contextStack, GameCache cache, string contextName="tags")
        {
            var context = new CommandContext(contextStack.Context, contextName);
            Populate(contextStack, context, cache);
            return context;
        }

        public static void Populate(CommandContextStack contextStack, CommandContext context, GameCache cache)
        {
            context.AddCommand(new TestCommand(cache));

            context.AddCommand(new DumpLogCommand());
            context.AddCommand(new RunCommands(contextStack));
            context.AddCommand(new ClearCommand());
            context.AddCommand(new EchoCommand());
            context.AddCommand(new HelpCommand(contextStack));
            context.AddCommand(new SetLocaleCommand());
            context.AddCommand(new StopwatchCommand());
            context.AddCommand(new ConvertPluginsCommand(cache));
            context.AddCommand(new ListTagsCommand(cache));
            context.AddCommand(new EditTagCommand(contextStack, cache));
            context.AddCommand(new GenerateCampaignFileCommand(cache));
            context.AddCommand(new NameTagCommand(cache));
            context.AddCommand(new ForEachCommand(contextStack, cache));
            context.AddCommand(new ListAllStringsCommand(cache));
            context.AddCommand(new StringIdCommand(cache));
            context.AddCommand(new GenerateAssemblyPluginsCommand());
            context.AddCommand(new DuplicateTagCommand(cache));
            context.AddCommand(new DeleteTagCommand(cache));
            context.AddCommand(new ListNullTagsCommand(cache));
            context.AddCommand(new ListUnnamedTagsCommand(cache));
            context.AddCommand(new ExtractBitmapsCommand(cache));

            // Halo Online Specific Commands
            if (cache is GameCacheHaloOnlineBase)
            {
                var hoCache = cache as GameCacheHaloOnlineBase;
                context.AddCommand(new SaveTagNamesCommand(hoCache));
                context.AddCommand(new SaveModdedTagsCommand(hoCache));
                context.AddCommand(new CreateTagCommand(hoCache));
                context.AddCommand(new ImportTagCommand(hoCache));
                context.AddCommand(new TagDependencyCommand(hoCache));
                context.AddCommand(new TagResourceCommand(hoCache));
                context.AddCommand(new ListUnusedTagsCommand(hoCache));
                context.AddCommand(new GetTagInfoCommand(hoCache));
                context.AddCommand(new GetTagAddressCommand());
                context.AddCommand(new ExtractTagCommand(hoCache));
                context.AddCommand(new ExtractAllTagsCommand(hoCache));
                context.AddCommand(new ExportTagModCommand(hoCache));
                context.AddCommand(new DumpDisassembledShadersCommand(hoCache));

                // modding commands
                context.AddCommand(new OpenModPackageCommand(contextStack, hoCache));
                context.AddCommand(new CreateCharacterType(cache));

                
                context.AddCommand(new UpdateMapFilesCommand(cache));

                context.AddCommand(new RescaleGUICommand(cache));
                context.AddCommand(new RescaleHudTextCommand(cache));
                context.AddCommand(new ImportFontsCommand(hoCache));

                context.AddCommand(new PhysicsModelTestCommand(cache));
                context.AddCommand(new CollisionModelTestCommand(hoCache));
                
            }

            if(cache is GameCacheHaloOnline)
            {
                var hoCache = cache as GameCacheHaloOnline;
                context.AddCommand(new RebuildCacheFileCommand(hoCache));
                context.AddCommand(new CreateModPackageCommand(contextStack, hoCache));
            }

            if (cache is GameCacheModPackage)
            {
                var modCache = cache as GameCacheModPackage;
                context.AddCommand(new SwitchTagCacheCommand(modCache));
                context.AddCommand(new ModCacheInfoCommand(modCache));
                context.AddCommand(new SaveModPackageCommand(modCache));
                context.AddCommand(new ExtractFontsCommand(modCache));
                context.AddCommand(new ApplyModPackageCommand(modCache));
                context.AddCommand(new AddTagCacheCommand(modCache));
                context.AddCommand(new DeleteTagCacheCommand(modCache));
                context.AddCommand(new AddModFilesCommand(modCache));
                context.AddCommand(new NameTagCacheCommand(modCache));
                context.AddCommand(new UpdateDescriptionCommand(modCache));
                context.AddCommand(new ModIsMainmenuCommand(modCache));
            }

            // porting related
            context.AddCommand(new UseXSDCommand());
            context.AddCommand(new UseAudioCacheCommand());
            context.AddCommand(new OpenCacheFileCommand(contextStack, cache));
        }
    }
}
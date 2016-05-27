define(["sitecore",
  "/-/speak/v1/ExperienceEditor/ExperienceEditor.js",
  "/-/speak/v1/ExperienceEditor/ExperienceEditor.Context.js"],
  function (Sitecore, ExperienceEditor, ExperienceEditorContext) {
      Sitecore.Commands.LaunchFieldEditorSetProject =
      {
          canExecute: function (context) {
              if (!ExperienceEditor.isInMode("edit")) {
                  return false;
              }
              return context.app.canExecute("ExperienceEditor.Projects.IsItemProjectable", context.currentContext);
          },
          execute: function (context) {
              context.currentContext.argument = context.button.viewModel.$el[0].accessKey;

              ExperienceEditor.PipelinesUtil.generateRequestProcessor("ExperienceEditor.Projects.GenerateFieldEditorUrlForProject", function (response) {
                  var DialogUrl = response.responseValue.value;
                  var dialogFeatures = "dialogHeight: 280px;dialogWidth: 520px;";
                  ExperienceEditor.Dialogs.showModalDialog(DialogUrl, '', dialogFeatures, null);
              }).execute(context);
          }
      };
  });
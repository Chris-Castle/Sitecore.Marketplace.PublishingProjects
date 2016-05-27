define(["sitecore", "/-/speak/v1/ExperienceEditor/ExperienceEditor.js"], function (Sitecore, ExperienceEditor) {
  Sitecore.Commands.AddProject =
  {
    canExecute: function (context) {
        if (!ExperienceEditor.isInMode("edit")) {
            return false;
        }
        return true;
    },

    execute: function (context) {
      ExperienceEditor.handleIsModified();

      var dialogFeatures = "dialogHeight: 460px;dialogWidth: 550px;";
      var dialogUrl = "/sitecore/shell/default.aspx?xmlcontrol=AddNewProject";

      ExperienceEditor.Dialogs.showModalDialog(dialogUrl, "", dialogFeatures, null, function (result) {
        if (!result) {
          return;
        }

        if (result != "yes") {
          return;
        }

        window.top.location.reload();
      });
    }
  };
});
define(["sitecore", "/-/speak/v1/ExperienceEditor/ExperienceEditor.js"], function (Sitecore, ExperienceEditor) {
    Sitecore.Commands.ViewProjectItems =
    {
        canExecute: function (context) {
            return context.app.canExecute("ExperienceEditor.Projects.IsInProject", context.currentContext);
        },

        execute: function (context) {
            ExperienceEditor.handleIsModified();

            var dialogFeatures = "dialogHeight: 560px;dialogWidth: 550px;";
            var dialogUrl = "/sitecore/shell/default.aspx?xmlcontrol=ProjectItems";
            dialogUrl += "&id=" + context.currentContext.itemId;
            dialogUrl += "&la=" + context.currentContext.language;
            dialogUrl += "&vs=" + context.currentContext.version;
            dialogUrl += "&db=" + context.currentContext.database;

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
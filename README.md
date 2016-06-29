# Sitecore.Marketplace.PublishingProjects

This module allows you to create publishing projects which let you manage the release date of a group of item versions.  This is accomplished by setting the publishing restrictions on the individual items when they are added to a project.

It includes additions to both content and experience editor to add projects, add versions to projects, view what versions are in a project, gutters, notifications, etc.

Since it relies on publishing restrictions, all Sitecore notifications and UIs such as preview work as expected.

SOme of this work was influenced by prior work by John Penfold.
https://marketplace.sitecore.net/Modules/P/Project_Items.aspx?sc_lang=en

The module by John was a little different in that it was designed to hold items until they were all approved.  My module instead simply relies on publishing restrictions.  It also works on versons, not items.  The use case we were solving was simply a little different.

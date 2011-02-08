Hello!

This is the code from the MvcCOnf Real World Application Development with Mvc3 NHibernate, FluentNHibernate and Castle Windsor.  These are my awesome notes for the presentation... think of them as a poor mans slide deck

The Castle version is 2.5.2, and the NHibernateFacility is a custom build, I will be making a pull request, so hopefull it will make it in, or something similar will soon


URLs

Castle
http://castleproject.org/
http://stw.castleproject.org/
http://groups.google.com/group/castle-project-users

NHibernate
http://nhforge.org/Default.aspx
http://groups.google.com/group/nhusers

FluentNHibernate
http://fluentnhibernate.org/
http://groups.google.com/group/fluent-nhibernate
http://stackoverflow.com/questions/tagged/fluent-nhibernate


AutoMapper
http://automapper.codeplex.com/

Thanks :D

@chriscanal


Mvc Configuration
----------------------
New Mvc DI goodness?
 - Mvc 1/2
	 - IControllerFactory
 - Mvc 3  - borked
	- IDependencyResolver
	- IControllerActivator
	- IViewActivator

 - Goals

Castle.Core
Castle.Windsor

global.asax
 - Registering installers

WindsorControllerFactory
 - GetControllerInstance
	 - UpdateRequestHost - will return to
 - ReleaseController

MvcControllerInstaller
 - BasedOn usage
 - If, can chain
 - Confguire
	- Lifestyle

MvcComponentsInstaller
 - Goals
 - *ContextHost
 	- WindsorControllerFactory UpdateRequestHost
 - UsingFactoryMethod
 - *Helper

----------------------
Logging
----------------------
 - LoggingInstaller
 - Castle.Facilities.Logging
	- Log4net
 	- NLog

----------------------
Persistence
----------------------
 - Goals
 - Tools
	- NHibernate
	- FluentNHibernate
	- NHibernateFacility
	- TransactionFacility

 - PersistenceInstaller
 - TransactionFacility
 - NHibernateFacility
 - IConfigurationBuilder/FluentNHibernateConfigurationBuilder
 - INHibernateConfiguration/DefaultNHibernateConfiguration
 	- RealWorldNHibernateConfiguration
		- DefaultStormConventions
		- ForeignKeyConvention
		- CategoryOverride

----------------------
AutoMapperInstaller
----------------------

 - Mapper.Initialize
 - RegisterMapperEngine
 - SetAutoMappedViewResultMapMethod
 - BrowseController - example

----------------------
Providers
----------------------
 - Can we squeeze in FluentMvc?
 - ModelBinderProvidersInstaller
 - Factory
 - ModelValidatorInstaller
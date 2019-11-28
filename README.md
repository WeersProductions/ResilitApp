# ResilitApp

The App that is used for both students and companies. Built to be compatible for both Android and iOS using Xamarin.forms. 

## Functionality

### Students
Can register and login. Login will be cached.
Can see the schedule and favorite talks. 
Can see their favorited talks in a schedule.

### Companies
Can register and login. Login will be cached.
Can scan students and add comments to them.

## Set-up
Clone the repo, open it using Visual Studio. Make sure all NuGet packages are properly downloaded as well. Be careful to not just update them, since they might have breaking changes.

## Requirements
SyncFusion is used for the schedule components and various optimized lists. The waiting indicator is also controlled by SyncFusion.
Since SNiC is non-profit, a legal key is free.

## Building
### Debugging
Visual Studio will show the correct build steps already for debugging. No special steps are needed. This can be either on a simulator or a physical device. Both work.

### Build
Visual Studio did not have support for the 'new' Android bundles. The build config has been changed for Android, to support this. This means that the standard 'build' procedure of Visual studio does not work for the Release version.
To solve this, run the following commands from the root of the project:
```
cd ./ResilITApp.Android/
msbuild -restore ResilITApp.Android.csproj -t:SignAndroidPackage -p:Configuration=Release -p:AndroidKeyStore=True -p:AndroidSigningKeyStore=[pathToKeyStore] -p:AndroidSigningStorePass=[StorePass] -p:AndroidSigningKeyAlias=ResilIT -p:AndroidSigningKeyPass=[StorePass]
```

The .aab file can be found at:
```
cd ./bin/Release/
```
and is named:
```
com.SNiC.ResilITApp-Signed.aab
```

You can upload this to the Google Play Console.

## TODO
The app is not yet finished.
- Users should be able to get and show their ticket using the app. It should also turn up the brightness.
- Users should be able to get an overview of the talks they enrolled for.
- Logout should be possible.
- Push notifications when a talk/speeddate is almost starting.

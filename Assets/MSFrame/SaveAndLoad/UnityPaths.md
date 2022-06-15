## IOS:
Application.dataPath : `Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/xxx.app/Data`
Application.streamingAssetsPath : `Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/xxx.app/Data/Raw`
Application.persistentDataPath : `Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/Documents`
Application.temporaryCachePath : `Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/Library/Caches`

## Android:
Application.dataPath : `/data/app/xxx.xxx.xxx.apk`
Application.streamingAssetsPath : ` jar:file:///data/app/xxx.xxx.xxx.apk/!/assets`
Application.persistentDataPath : /data/data/xxx.xxx.xxx/files
Application.temporaryCachePath : /data/data/xxx.xxx.xxx/cache

## Windows:
Application.dataPath : ` E:/UnityProject/Assets`
Application.streamingAssetsPath : `E:/UnityProject/Assets/StreamingAssets`
Application.persistentDataPath : `C:/Users/username/AppData/LocalLow/DefaultCompany/UnityProject`
Application.temporaryCachePath : `C:/Users/username/AppData/Local/Temp/DefaultCompany/UnityProject`

## 注意：
Application.persistentDataPath 才是移动端可用的保存生成文件的地方
放到resource中打包后不可以更改了
放到Application .dataPath中移动端是没有访问权限的
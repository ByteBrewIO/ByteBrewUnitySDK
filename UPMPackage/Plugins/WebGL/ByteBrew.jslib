mergeInto(LibraryManager.library, {

  InitializeByteBrew: function (appID, appKey, appVersion) {
    var appIDStr = UTF8ToString(appID);
    var appKeyStr = UTF8ToString(appKey);
    var appVersionStr = UTF8ToString(appVersion);

    ByteBrewSDK.ByteBrew.initializeByteBrew(appIDStr, appKeyStr, appVersionStr);
  },

  IsByteBrewInitialized: function () {
    return ByteBrewSDK.ByteBrew.isByteBrewInitialized();
  },

  NewCustomEvent: function (eventName) {
    var eventNameStr = UTF8ToString(eventName);

    ByteBrewSDK.ByteBrew.newCustomEvent(eventNameStr);
  },

  NewCustomEventWithString: function (eventName, value) {
    var eventNameStr = UTF8ToString(eventName);
    var valueStr = UTF8ToString(value);

    ByteBrewSDK.ByteBrew.newCustomEvent(eventNameStr, valueStr);
  },

  NewCustomEventWithNumber: function (eventName, value) {
    var eventNameStr = UTF8ToString(eventName);

    ByteBrewSDK.ByteBrew.newCustomEvent(eventNameStr, value);
  },

  LoadRemoteConfigs: function () {
    ByteBrewSDK.ByteBrew.loadRemoteConfigs(() => console.log("Remote configs loaded!"));
  },

  RetreiveRemoteConfigValue: function (key, defaultValue) {
    var keyStr = UTF8ToString(key);
    var defaultValueStr = UTF8ToString(defaultValue);
    var retreivedValue = ByteBrewSDK.ByteBrew.retreiveRemoteConfigValue(keyStr, defaultValueStr);
    var bufferSize = lengthBytesUTF8(retreivedValue) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(retreivedValue, buffer, bufferSize);
    return buffer;
  },

  HasRemoteConfigsBeenSet: function () {
    return ByteBrewSDK.ByteBrew.hasRemoteConfigsBeenSet();
  },

  RestartTracking: function () {
    ByteBrewSDK.ByteBrew.restartTracking();
  },

  StopTracking: function () {
    ByteBrewSDK.ByteBrew.stopTracking();
  },

  GetUserID: function () {
    var userID = ByteBrewSDK.ByteBrew.getUserID();
    var bufferSize = lengthBytesUTF8(userID) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(userID, buffer, bufferSize);
    return buffer;
  },

});
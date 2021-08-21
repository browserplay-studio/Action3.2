mergeInto(LibraryManager.library, {
  InitializePlayer: function() {
    auth();
  },
  GetLeaderboardEntries: function (tableName) {
    //console.log(".jslib GetLeaderboardEntries");
    tableName = Pointer_stringify(tableName);
    getLeaderboardEntries(tableName);
  },
  SetLeaderboardScore: function(tableName, score) {
    //console.log(".jslib SetLeaderboardScore");
    tableName = Pointer_stringify(tableName);
    setLeaderboardScore(tableName, score);
  },
  GetTableByIndex: function(index) {
    var tableName = getTableNameByIndex(index);
    console.log(".jslib GetTableByIndex", tableName);
    var bufferSize = lengthBytesUTF8(tableName) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(tableName, buffer, bufferSize);
    return buffer;
  }
});

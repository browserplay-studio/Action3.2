
let sdk;
let player;

const options = {
  adv: {
    onAdvClose: (wasShown) => {
      console.log(`adv closed! ${wasShown}`);
    }
  }
};

YaGames.init(options).then(ysdk => {
  sdk = ysdk;
  // ! from unity
  //auth();
});

function auth() {
  sdk.auth.openAuthDialog()
    .then(() => {
      // Игрок успешно авторизован, теперь объект Player будет инициализирован.
      initPlayer();
      console.log("auth ok");
      unityInstance.SendMessage("[Yandex SDK]", "HandlePlayerInit", 1);
    })
    .catch(() => {
      // Игрок не авторизован.
      console.log("auth failed");
      unityInstance.SendMessage("[Yandex SDK]", "HandlePlayerInit", 0);
    });
}

function initPlayer() {
  sdk.getPlayer()
    .then(p => {
      player = p;
      console.log("Player:", player);
    })
    .catch(error => {
      // Если игрок не авторизован, выбрасывает исключение USER_NOT_AUTHORIZED
      console.log(error);
    });
}

function getLeaderboardEntries(tableName) {
  sdk.getLeaderboards().then(leaderboard => {
    const config = {
      includeUser: true,
      quantityAround: 5,
      quantityTop: 20
    };
  
    leaderboard.getLeaderboardEntries(tableName, config)
      .then(result => { 
        const entries = result["entries"].map(entry => {
          return {
            name: entry.player.publicName,
            score: entry.score
          }
        });
        console.log(entries);
        if (unityInstance) {
          const message = JSON.stringify({tableName, entries});
          unityInstance.SendMessage("[Yandex SDK]", "HandleLeaderboard", message);
        }
        else {
          alert("unityInstance doesn't found!");
        }
      });
  });
}

function setLeaderboardScore(tableName, score) {
  
  const overrideScore = true;

  if (overrideScore) {
    sdk.getLeaderboards().then(leaderboard => {
      leaderboard.setLeaderboardScore(tableName, score);
    });
  }
  else {
    setLeaderboardTopScore(tableName, score);
  }
}

async function setLeaderboardTopScore(tableName, score) {
  const leaderboard = await sdk.getLeaderboards();
  const descRes = await leaderboard.getLeaderboardDescription(tableName);

  leaderboard.getLeaderboardPlayerEntry(tableName).then(result => {
    
    //const type = descRes.description.type;
    const type = getTableType(tableName);
    const value = result.score;
    
    let updated = false;

    if (type === "numeric") {
      // set new score if it greater that existing one
      if (score > value) {
        leaderboard.setLeaderboardScore(tableName, score);
        updated = true;
      }
    }
    else if (type === "time") {
      // set new score if it lower than existing one
      if (score < value) {
        leaderboard.setLeaderboardScore(tableName, score);
        updated = true;
      }
    }

    console.log(type, updated, value, score);

  }).catch(err => {
    if (err.code === 'LEADERBOARD_PLAYER_NOT_PRESENT') {
      // Срабатывает, если у игрока нет записи в соревновательной таблице

      // set score
      leaderboard.setLeaderboardScore(tableName, score);
    }
    console.warn("setLeaderboardScore", err);
  });
}

function getTableType(tableName) {
  if (tableName.includes("numeric")) {
    return "numeric";
  }
  else if (tableName.includes("time")) {
    return "time";
  }
  else {
    console.log("sdk error: table name type");
  }
}

function getTableNameByIndex(index) {
  const names = [
    "table1time",
    "table2time",
    "table3time",
    "table4time",
    "table5time",
    "table6time",
    "table7time",


  ];

  if (index < 0 || index >= names.length) {
    index %= names.length;
    console.log("why are you gay");
  }

  return names[index];
}

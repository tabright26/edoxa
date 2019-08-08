export const LOAD_USERS = "LOAD_USERS";
export const LOAD_USERS_SUCCESS = "LOAD_USERS_SUCCESS";
export const LOAD_USERS_FAIL = "LOAD_USERS_FAIL";
export function loadUsers() {
  return {
    types: [LOAD_USERS, LOAD_USERS_SUCCESS, LOAD_USERS_FAIL],
    payload: {
      request: {
        method: "get",
        url: "/identity/api/users"
      }
    }
  };
}

export const LOAD_USER_GAMES = "LOAD_USER_GAMES";
export const LOAD_USER_GAMES_SUCCESS = "LOAD_USER_GAMES_SUCCESS";
export const LOAD_USER_GAMES_FAIL = "LOAD_USER_GAMES_FAIL";
export function loadUserGames() {
  return {
    types: [LOAD_USER_GAMES, LOAD_USER_GAMES_SUCCESS, LOAD_USER_GAMES_FAIL],
    payload: {
      request: {
        method: "get",
        url: "/identity/api/games"
      }
    }
  };
}

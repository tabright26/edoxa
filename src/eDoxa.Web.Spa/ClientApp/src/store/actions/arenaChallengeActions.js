export const LOAD_CHALLENGES = 'LOAD_CHALLENGES';
export const LOAD_CHALLENGES_SUCCESS = 'LOAD_CHALLENGES_SUCCESS';
export const LOAD_CHALLENGES_FAIL = 'LOAD_CHALLENGES_FAIL';
export function loadChallenges() {
  return function(dispatch, getState) {
    dispatch({
      types: [LOAD_CHALLENGES, LOAD_CHALLENGES_SUCCESS, LOAD_CHALLENGES_FAIL],
      payload: {
        request: {
          method: 'get',
          url: '/arena/challenges/api/challenges',
          headers: {
            authorization: `Bearer ${getState().oidc.user.access_token}`,
            accept: 'application/json'
          }
        }
      }
    });
  };
}

export const LOAD_CHALLENGE = 'LOAD_CHALLENGE';
export const LOAD_CHALLENGE_SUCCESS = 'LOAD_CHALLENGE_SUCCESS';
export const LOAD_CHALLENGE_FAIL = 'LOAD_CHALLENGE_FAIL';
export function loadChallenge(challengeId) {
  return async function(dispatch, getState) {
    dispatch({
      types: [LOAD_CHALLENGE, LOAD_CHALLENGE_SUCCESS, LOAD_CHALLENGE_FAIL],
      payload: {
        request: {
          method: 'get',
          url: `/arena/challenges/api/challenges/${challengeId}`,
          headers: {
            authorization: `Bearer ${getState().oidc.user.access_token}`,
            accept: 'application/json'
          }
        }
      }
    });
  };
}

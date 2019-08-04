import { findChallengeAsync } from '../../services/arenaChallengeService';

export const FETCH_CHALLENGES_SUCCESS = 'FETCH_CHALLENGES_SUCCESS';
export const FIND_CHALLENGE_SUCCESS = 'FIND_CHALLENGE_SUCCESS';

export const LOAD_CHALLENGES = 'LOAD_CHALLENGES';
export const LOAD_CHALLENGES_SUCCESS = 'LOAD_CHALLENGES_SUCCESS';
export const LOAD_CHALLENGES_FAIL = 'LOAD_CHALLENGES_FAIL';

export function findChallengeSuccess(challenge) {
  return { type: FIND_CHALLENGE_SUCCESS, challenge };
}

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

export function findChallenge(challengeId) {
  return async function(dispatch, getState) {
    try {
      const response = await findChallengeAsync(challengeId, getState);
      dispatch(findChallengeSuccess(response.data));
    } catch (error) {
      console.log(error);
    }
  };
}

import {
  fetchChallengesAsync,
  findChallengeAsync
} from '../../services/arenaChallengesService';

export const FETCH_CHALLENGES_SUCCESS = 'FETCH_CHALLENGES_SUCCESS';
export const FIND_CHALLENGE_SUCCESS = 'FIND_CHALLENGE_SUCCESS';

export function fetchChallengesSuccess(challenges) {
  return { type: FETCH_CHALLENGES_SUCCESS, challenges };
}

export function findChallengeSuccess(challenge) {
  return { type: FIND_CHALLENGE_SUCCESS, challenge };
}

export function fetchChallenges() {
  return async function(dispatch, getState) {
    try {
      const response = await fetchChallengesAsync(getState);
      dispatch(fetchChallengesSuccess(response.data));
    } catch (error) {
      console.log(error);
    }
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

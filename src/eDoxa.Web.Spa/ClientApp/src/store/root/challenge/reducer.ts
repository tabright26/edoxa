import {
  LOAD_CHALLENGES,
  LOAD_CHALLENGES_SUCCESS,
  LOAD_CHALLENGES_FAIL,
  LOAD_CHALLENGE,
  LOAD_CHALLENGE_SUCCESS,
  LOAD_CHALLENGE_FAIL,
  REGISTER_CHALLENGE_PARTICIPANT,
  REGISTER_CHALLENGE_PARTICIPANT_SUCCESS,
  REGISTER_CHALLENGE_PARTICIPANT_FAIL,
  ChallengesActions
} from "store/actions/challenge/types";
import { Reducer } from "redux";
import produce, { Draft } from "immer";
import { ChallengesState } from "./types";

export const initialState: ChallengesState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<ChallengesState, ChallengesActions> = produce(
  (draft: Draft<ChallengesState>, action: ChallengesActions) => {
    switch (action.type) {
      case LOAD_CHALLENGES:
        draft.error = null;
        draft.loading = true;
        break;
      case LOAD_CHALLENGES_SUCCESS:
        const { status, data } = action.payload;
        switch (status) {
          case 204:
            draft.error = null;
            draft.loading = false;
            break;
          default:
            draft.data = data;
            draft.error = null;
            draft.loading = false;
            break;
        }
        break;
      case LOAD_CHALLENGES_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
      case LOAD_CHALLENGE:
        draft.error = null;
        draft.loading = true;
        break;
      case LOAD_CHALLENGE_SUCCESS: {
        const challenge = action.payload.data;
        const index = draft.data.findIndex(x => x.id === challenge.id);
        index === -1
          ? draft.data.push(challenge)
          : (draft.data[index] = challenge);
        draft.error = null;
        draft.loading = false;
        break;
      }
      case LOAD_CHALLENGE_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
      case REGISTER_CHALLENGE_PARTICIPANT:
        draft.error = null;
        draft.loading = true;
        break;
      case REGISTER_CHALLENGE_PARTICIPANT_SUCCESS: {
        const participant = action.payload.data;
        const challenge = draft.data.find(
          x => x.id === participant.challengeId
        );
        challenge.participants.push(participant);
        draft.error = null;
        draft.loading = false;
        break;
      }
      case REGISTER_CHALLENGE_PARTICIPANT_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
    }
  },
  initialState
);

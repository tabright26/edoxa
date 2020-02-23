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
  LOAD_CHALLENGE_HISTORY,
  LOAD_CHALLENGE_HISTORY_SUCCESS,
  LOAD_CHALLENGE_HISTORY_FAIL
} from "store/actions/challenge/types";
import { Reducer } from "redux";
import produce, { Draft } from "immer";
import { ChallengesState } from "./types";
import { RootActions } from "store/types";

export const initialState: ChallengesState = {
  data: [],
  loading: false
};

export const reducer: Reducer<ChallengesState, RootActions> = produce(
  (draft: Draft<ChallengesState>, action: RootActions) => {
    switch (action.type) {
      case LOAD_CHALLENGES:
        draft.loading = true;
        break;
      case LOAD_CHALLENGES_SUCCESS: {
        const { status, data } = action.payload;
        switch (status) {
          case 204: {
            draft.loading = false;
            break;
          }
          default: {
            data.forEach(challenge => {
              const index = draft.data.findIndex(x => x.id === challenge.id);
              if (index === -1) {
                draft.data.push(challenge);
              } else {
                draft.data[index] = challenge;
              }
            });
            draft.loading = false;
            break;
          }
        }
        break;
      }
      case LOAD_CHALLENGES_FAIL:
        draft.loading = false;
        break;
      case LOAD_CHALLENGE_HISTORY:
        draft.loading = true;
        break;
      case LOAD_CHALLENGE_HISTORY_SUCCESS: {
        const { status, data } = action.payload;
        switch (status) {
          case 204: {
            draft.loading = false;
            break;
          }
          default: {
            data.forEach(challenge => {
              const index = draft.data.findIndex(x => x.id === challenge.id);
              if (index === -1) {
                draft.data.push(challenge);
              } else {
                draft.data[index] = challenge;
              }
            });
            draft.loading = false;
            break;
          }
        }
        break;
      }
      case LOAD_CHALLENGE_HISTORY_FAIL:
        draft.loading = false;
        break;
      case LOAD_CHALLENGE:
        draft.loading = true;
        break;
      case LOAD_CHALLENGE_SUCCESS: {
        const challenge = action.payload.data;
        const index = draft.data.findIndex(x => x.id === challenge.id);
        index === -1
          ? draft.data.push(challenge)
          : (draft.data[index] = challenge);
        draft.loading = false;
        break;
      }
      case LOAD_CHALLENGE_FAIL:
        draft.loading = false;
        break;
      case REGISTER_CHALLENGE_PARTICIPANT:
        draft.loading = true;
        break;
      case REGISTER_CHALLENGE_PARTICIPANT_SUCCESS: {
        const participant = action.payload.data;
        draft.data
          .find(challenge => challenge.id === participant.challengeId)
          .participants.push(participant);
        draft.loading = false;
        break;
      }
      case REGISTER_CHALLENGE_PARTICIPANT_FAIL:
        draft.loading = false;
        break;
    }
  },
  initialState
);

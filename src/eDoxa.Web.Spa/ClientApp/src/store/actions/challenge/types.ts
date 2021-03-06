import { AxiosActionCreator, AxiosAction } from "utils/axios/types";
import { Challenge, ChallengeParticipant } from "types/challenges";

export const LOAD_CHALLENGES = "LOAD_CHALLENGES";
export const LOAD_CHALLENGES_SUCCESS = "LOAD_CHALLENGES_SUCCESS";
export const LOAD_CHALLENGES_FAIL = "LOAD_CHALLENGES_FAIL";

export const LOAD_CHALLENGE_HISTORY = "LOAD_CHALLENGE_HISTORY";
export const LOAD_CHALLENGE_HISTORY_SUCCESS = "LOAD_CHALLENGE_HISTORY_SUCCESS";
export const LOAD_CHALLENGE_HISTORY_FAIL = "LOAD_CHALLENGE_HISTORY_FAIL";

export const LOAD_CHALLENGE = "LOAD_CHALLENGE";
export const LOAD_CHALLENGE_SUCCESS = "LOAD_CHALLENGE_SUCCESS";
export const LOAD_CHALLENGE_FAIL = "LOAD_CHALLENGE_FAIL";

export const REGISTER_CHALLENGE_PARTICIPANT = "REGISTER_CHALLENGE_PARTICIPANT";
export const REGISTER_CHALLENGE_PARTICIPANT_SUCCESS =
  "REGISTER_CHALLENGE_PARTICIPANT_SUCCESS";
export const REGISTER_CHALLENGE_PARTICIPANT_FAIL =
  "REGISTER_CHALLENGE_PARTICIPANT_FAIL";

export type LoadChallengesType =
  | typeof LOAD_CHALLENGES
  | typeof LOAD_CHALLENGES_SUCCESS
  | typeof LOAD_CHALLENGES_FAIL;
export type LoadChallengesActionCreator = AxiosActionCreator<
  LoadChallengesType
>;
export type LoadChallengesAction = AxiosAction<LoadChallengesType, Challenge[]>;

export type LoadChallengeHistoryType =
  | typeof LOAD_CHALLENGE_HISTORY
  | typeof LOAD_CHALLENGE_HISTORY_SUCCESS
  | typeof LOAD_CHALLENGE_HISTORY_FAIL;
export type LoadChallengeHistoryActionCreator = AxiosActionCreator<
  LoadChallengeHistoryType
>;
export type LoadChallengeHistoryAction = AxiosAction<
  LoadChallengeHistoryType,
  Challenge[]
>;

export type LoadChallengeType =
  | typeof LOAD_CHALLENGE
  | typeof LOAD_CHALLENGE_SUCCESS
  | typeof LOAD_CHALLENGE_FAIL;
export type LoadChallengeActionCreator = AxiosActionCreator<LoadChallengeType>;
export type LoadChallengeAction = AxiosAction<LoadChallengeType, Challenge>;

export type RegisterChallengeParticipantType =
  | typeof REGISTER_CHALLENGE_PARTICIPANT
  | typeof REGISTER_CHALLENGE_PARTICIPANT_SUCCESS
  | typeof REGISTER_CHALLENGE_PARTICIPANT_FAIL;
export type RegisterChallengeParticipantActionCreator = AxiosActionCreator<
  RegisterChallengeParticipantType
>;
export type RegisterChallengeParticipantAction = AxiosAction<
  RegisterChallengeParticipantType,
  ChallengeParticipant
>;

import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";
import { Candidature } from "types";

export const LOAD_CLAN_CANDIDATURES = "LOAD_CLAN_CANDIDATURES";
export const LOAD_CLAN_CANDIDATURES_SUCCESS = "LOAD_CLAN_CANDIDATURES_SUCCESS";
export const LOAD_CLAN_CANDIDATURES_FAIL = "LOAD_CLAN_CANDIDATURES_FAIL";

export const LOAD_CLAN_CANDIDATURE = "LOAD_CLAN_CANDIDATURE";
export const LOAD_CLAN_CANDIDATURE_SUCCESS = "LOAD_CLAN_CANDIDATURE_SUCCESS";
export const LOAD_CLAN_CANDIDATURE_FAIL = "LOAD_CLAN_CANDIDATURE_FAIL";

export const SEND_CLAN_CANDIDATURE = "SEND_CLAN_CANDIDATURE";
export const SEND_CLAN_CANDIDATURE_SUCCESS = "SEND_CLAN_CANDIDATURE_SUCCESS";
export const SEND_CLAN_CANDIDATURE_FAIL = "SEND_CLAN_CANDIDATURE_FAIL";

export const ACCEPT_CLAN_CANDIDATURE = "ACCEPT_CLAN_CANDIDATURE";
export const ACCEPT_CLAN_CANDIDATURE_SUCCESS = "ACCEPT_CLAN_CANDIDATURE_SUCCESS";
export const ACCEPT_CLAN_CANDIDATURE_FAIL = "ACCEPT_CLAN_CANDIDATURE_FAIL";

export const REFUSE_CLAN_CANDIDATURE = "REFUSE_CLAN_CANDIDATURE";
export const REFUSE_CLAN_CANDIDATURE_SUCCESS = "REFUSE_CLAN_CANDIDATURE_SUCCESS";
export const REFUSE_CLAN_CANDIDATURE_FAIL = "REFUSE_CLAN_CANDIDATURE_FAIL";

export type LoadClanCandidaturesdType = typeof LOAD_CLAN_CANDIDATURES | typeof LOAD_CLAN_CANDIDATURES_SUCCESS | typeof LOAD_CLAN_CANDIDATURES_FAIL;
export type LoadClanCandidaturesActionCreator = AxiosActionCreator<LoadClanCandidaturesdType>;
export type LoadClanCandidaturesAction = AxiosAction<LoadClanCandidaturesdType, Candidature[]>;

export type LoadClanCandidatureType = typeof LOAD_CLAN_CANDIDATURE | typeof LOAD_CLAN_CANDIDATURE_SUCCESS | typeof LOAD_CLAN_CANDIDATURE_FAIL;
export type LoadClanCandidatureActionCreator = AxiosActionCreator<LoadClanCandidatureType>;
export type LoadClanCandidatureAction = AxiosAction<LoadClanCandidatureType, Candidature>;

export type SendClanCandidatureType = typeof SEND_CLAN_CANDIDATURE | typeof SEND_CLAN_CANDIDATURE_SUCCESS | typeof SEND_CLAN_CANDIDATURE_FAIL;
export type SendClanCandidatureActionCreator = AxiosActionCreator<SendClanCandidatureType>;
export type SendClanCandidatureAction = AxiosAction<SendClanCandidatureType, Candidature>;

export type AcceptClanCandidatureType = typeof ACCEPT_CLAN_CANDIDATURE | typeof ACCEPT_CLAN_CANDIDATURE_SUCCESS | typeof ACCEPT_CLAN_CANDIDATURE_FAIL;
export type AcceptClanCandidatureActionCreator = AxiosActionCreator<AcceptClanCandidatureType>;
export type AcceptClanCandidatureAction = AxiosAction<AcceptClanCandidatureType, Candidature>;

export type RefuseClanCandidatureType = typeof REFUSE_CLAN_CANDIDATURE | typeof REFUSE_CLAN_CANDIDATURE_SUCCESS | typeof REFUSE_CLAN_CANDIDATURE_FAIL;
export type RefuseClanCandidatureActionCreator = AxiosActionCreator<RefuseClanCandidatureType>;
export type RefuseClanCandidatureAction = AxiosAction<RefuseClanCandidatureType, Candidature>;

export type ClanCandidaturesActionCreators =
  | LoadClanCandidaturesActionCreator
  | LoadClanCandidatureActionCreator
  | SendClanCandidatureActionCreator
  | AcceptClanCandidatureActionCreator
  | RefuseClanCandidatureActionCreator;
export type ClanCandidaturesActions = LoadClanCandidaturesAction | LoadClanCandidatureAction | SendClanCandidatureAction | AcceptClanCandidatureAction | RefuseClanCandidatureAction;
export type ClanCandidaturesState = AxiosState<Candidature[]>;

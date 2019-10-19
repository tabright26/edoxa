import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";

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

type LoadClanCandidaturesdType = typeof LOAD_CLAN_CANDIDATURES | typeof LOAD_CLAN_CANDIDATURES_SUCCESS | typeof LOAD_CLAN_CANDIDATURES_FAIL;

interface LoadClanCandidaturesActionCreator extends AxiosActionCreator<LoadClanCandidaturesdType> {}

interface LoadClanCandidaturesAction extends AxiosAction<LoadClanCandidaturesdType> {}

type LoadClanCandidatureType = typeof LOAD_CLAN_CANDIDATURE | typeof LOAD_CLAN_CANDIDATURE_SUCCESS | typeof LOAD_CLAN_CANDIDATURE_FAIL;

interface LoadClanCandidatureActionCreator extends AxiosActionCreator<LoadClanCandidatureType> {}

interface LoadClanCandidatureAction extends AxiosAction<LoadClanCandidatureType> {}

type SendClanCandidatureType = typeof SEND_CLAN_CANDIDATURE | typeof SEND_CLAN_CANDIDATURE_SUCCESS | typeof SEND_CLAN_CANDIDATURE_FAIL;

interface SendClanCandidatureActionCreator extends AxiosActionCreator<SendClanCandidatureType> {}

interface SendClanCandidatureAction extends AxiosAction<SendClanCandidatureType> {}

type AcceptClanCandidatureType = typeof ACCEPT_CLAN_CANDIDATURE | typeof ACCEPT_CLAN_CANDIDATURE_SUCCESS | typeof ACCEPT_CLAN_CANDIDATURE_FAIL;

interface AcceptClanCandidatureActionCreator extends AxiosActionCreator<AcceptClanCandidatureType> {}

interface AcceptClanCandidatureAction extends AxiosAction<AcceptClanCandidatureType> {}

type RefuseClanCandidatureType = typeof REFUSE_CLAN_CANDIDATURE | typeof REFUSE_CLAN_CANDIDATURE_SUCCESS | typeof REFUSE_CLAN_CANDIDATURE_FAIL;

interface RefuseClanCandidatureActionCreator extends AxiosActionCreator<RefuseClanCandidatureType> {}

interface RefuseClanCandidatureAction extends AxiosAction<RefuseClanCandidatureType> {}

export type ClanCandidaturesActionCreators = LoadClanCandidaturesActionCreator | LoadClanCandidatureActionCreator | SendClanCandidatureActionCreator | AcceptClanCandidatureActionCreator | RefuseClanCandidatureActionCreator;

export type ClanCandidaturesActions = LoadClanCandidaturesAction | LoadClanCandidatureAction | SendClanCandidatureAction | AcceptClanCandidatureAction | RefuseClanCandidatureAction;

export type ClanCandidaturesState = AxiosState;

import { AxiosActionCreator, AxiosAction } from "store/middlewares/axios/types";

export const LOAD_CANDIDATURES = "LOAD_CANDIDATURES";
export const LOAD_CANDIDATURES_SUCCESS = "LOAD_CANDIDATURES_SUCCESS";
export const LOAD_CANDIDATURES_FAIL = "LOAD_CANDIDATURES_FAIL";

export const LOAD_CANDIDATURE = "LOAD_CANDIDATURE";
export const LOAD_CANDIDATURE_SUCCESS = "LOAD_CANDIDATURE_SUCCESS";
export const LOAD_CANDIDATURE_FAIL = "LOAD_CANDIDATURE_FAIL";

export const ADD_CANDIDATURE = "ADD_CANDIDATURE";
export const ADD_CANDIDATURE_SUCCESS = "ADD_CANDIDATURE_SUCCESS";
export const ADD_CANDIDATURE_FAIL = "ADD_CANDIDATURE_FAIL";

export const ACCEPT_CANDIDATURE = "ACCEPT_CANDIDATURE";
export const ACCEPT_CANDIDATURE_SUCCESS = "ACCEPT_CANDIDATURE_SUCCESS";
export const ACCEPT_CANDIDATURE_FAIL = "ACCEPT_CANDIDATURE_FAIL";

export const DECLINE_CANDIDATURE = "DECLINE_CANDIDATURE";
export const DECLINE_CANDIDATURE_SUCCESS = "DECLINE_CANDIDATURE_SUCCESS";
export const DECLINE_CANDIDATURE_FAIL = "DECLINE_CANDIDATURE_FAIL";

type LoadCandidaturesdType = typeof LOAD_CANDIDATURES | typeof LOAD_CANDIDATURES_SUCCESS | typeof LOAD_CANDIDATURES_FAIL;

interface LoadCandidaturesActionCreator extends AxiosActionCreator<LoadCandidaturesdType> {}

interface LoadCandidaturesAction extends AxiosAction<LoadCandidaturesdType> {}

//---------------------------------------------------------------------------------------------------

type LoadCandidatureType = typeof LOAD_CANDIDATURE | typeof LOAD_CANDIDATURE_SUCCESS | typeof LOAD_CANDIDATURE_FAIL;

interface LoadCandidatureActionCreator extends AxiosActionCreator<LoadCandidatureType> {}

interface LoadCandidatureAction extends AxiosAction<LoadCandidatureType> {}

//---------------------------------------------------------------------------------------------------

type AddCandidatureType = typeof ADD_CANDIDATURE | typeof ADD_CANDIDATURE_SUCCESS | typeof ADD_CANDIDATURE_FAIL;

interface AddCandidatureActionCreator extends AxiosActionCreator<AddCandidatureType> {}

interface AddCandidatureAction extends AxiosAction<AddCandidatureType> {}

//---------------------------------------------------------------------------------------------------

type AcceptCandidatureType = typeof ACCEPT_CANDIDATURE | typeof ACCEPT_CANDIDATURE_SUCCESS | typeof ACCEPT_CANDIDATURE_FAIL;

interface AcceptCandidatureActionCreator extends AxiosActionCreator<AcceptCandidatureType> {}

interface AcceptCandidatureAction extends AxiosAction<AcceptCandidatureType> {}

//---------------------------------------------------------------------------------------------------

type DeclineCandidatureType = typeof DECLINE_CANDIDATURE | typeof DECLINE_CANDIDATURE_SUCCESS | typeof DECLINE_CANDIDATURE_FAIL;

interface DeclineCandidatureActionCreator extends AxiosActionCreator<DeclineCandidatureType> {}

interface DeclineCandidatureAction extends AxiosAction<DeclineCandidatureType> {}

//---------------------------------------------------------------------------------------------------

export type CandidaturesActionCreators = LoadCandidaturesActionCreator | LoadCandidatureActionCreator | AddCandidatureActionCreator | AcceptCandidatureActionCreator | DeclineCandidatureActionCreator;

export type CandidaturesActionTypes = LoadCandidaturesAction | LoadCandidatureAction | AddCandidatureAction | AcceptCandidatureAction | DeclineCandidatureAction;

export interface CandidaturesState {}

import { AxiosActionCreator, AxiosAction } from "interfaces/axios";

export const LOAD_CANDIDATURES = "LOAD_CANDIDATURES";
export const LOAD_CANDIDATURES_SUCCESS = "LOAD_CANDIDATURES_SUCCESS";
export const LOAD_CANDIDATURES_FAIL = "LOAD_CANDIDATURES_FAIL";

type LoadCandidaturesType = typeof LOAD_CANDIDATURES | typeof LOAD_CANDIDATURES_SUCCESS | typeof LOAD_CANDIDATURES_FAIL;

interface LoadCandidaturesActionCreator extends AxiosActionCreator<LoadCandidaturesType> {}

interface LoadCandidaturesAction extends AxiosAction<LoadCandidaturesType> {}

export type CandidaturesActionCreators = LoadCandidaturesActionCreator;

export type CandidaturesActionTypes = LoadCandidaturesAction;

export interface CandidaturesState {}

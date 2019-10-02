import { AxiosActionCreator, AxiosAction } from "interfaces/axios";

export const LOAD_MEMBERS = "LOAD_MEMBERS";
export const LOAD_MEMBERS_SUCCESS = "LOAD_MEMBERS_FAIL";
export const LOAD_MEMBERS_FAIL = "LOAD_MEMBERS_FAIL";

type LoadMembersType = typeof LOAD_MEMBERS | typeof LOAD_MEMBERS_SUCCESS | typeof LOAD_MEMBERS_FAIL;

interface LoadMembersActionCreator extends AxiosActionCreator<LoadMembersType> {}

interface LoadMembersAction extends AxiosAction<LoadMembersType> {}

export type MembersActionCreators = LoadMembersActionCreator;

export type MembersActionTypes = LoadMembersAction;

export interface MembersState {}

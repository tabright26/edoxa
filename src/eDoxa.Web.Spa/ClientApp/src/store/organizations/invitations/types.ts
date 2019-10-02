import { AxiosActionCreator, AxiosAction } from "interfaces/axios";

export const LOAD_INVITATIONS = "LOAD_INVITATIONS";
export const LOAD_INVITATIONS_SUCCESS = "LOAD_INVITATIONS_SUCCESS";
export const LOAD_INVITATIONS_FAIL = "LOAD_INVITATIONS_FAIL";

type LoadInvitationsType = typeof LOAD_INVITATIONS | typeof LOAD_INVITATIONS_SUCCESS | typeof LOAD_INVITATIONS_FAIL;

interface LoadInvitationsActionCreator extends AxiosActionCreator<LoadInvitationsType> {}

interface LoadInvitationsAction extends AxiosAction<LoadInvitationsType> {}

export type InvitationsActionCreators = LoadInvitationsActionCreator;

export type InvitationsActionTypes = LoadInvitationsAction;

export interface InvitationsState {}

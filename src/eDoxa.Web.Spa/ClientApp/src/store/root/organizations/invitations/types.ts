import { AxiosActionCreator, AxiosAction } from "store/middlewares/axios/types";

export const LOAD_INVITATIONS = "LOAD_INVITATIONS";
export const LOAD_INVITATIONS_SUCCESS = "LOAD_INVITATIONS_SUCCESS";
export const LOAD_INVITATIONS_FAIL = "LOAD_INVITATIONS_FAIL";

export const LOAD_INVITATION = "LOAD_INVITATION";
export const LOAD_INVITATION_SUCCESS = "LOAD_INVITATION_SUCCESS";
export const LOAD_INVITATION_FAIL = "LOAD_INVITATION_FAIL";

export const ADD_INVITATION = "ADD_INVITATION";
export const ADD_INVITATION_SUCCESS = "ADD_INVITATION_SUCCESS";
export const ADD_INVITATION_FAIL = "ADD_INVITATION_FAIL";

export const ACCEPT_INVITATION = "ACCEPT_INVITATION";
export const ACCEPT_INVITATION_SUCCESS = "ACCEPT_INVITATION_SUCCESS";
export const ACCEPT_INVITATION_FAIL = "ACCEPT_INVITATION_FAIL";

export const DECLINE_INVITATION = "DECLINE_INVITATION";
export const DECLINE_INVITATION_SUCCESS = "DECLINE_INVITATION_SUCCESS";
export const DECLINE_INVITATION_FAIL = "DECLINE_INVITATION_FAIL";

type LoadInvitationsType = typeof LOAD_INVITATIONS | typeof LOAD_INVITATIONS_SUCCESS | typeof LOAD_INVITATIONS_FAIL;

interface LoadInvitationsActionCreator extends AxiosActionCreator<LoadInvitationsType> {}

interface LoadInvitationsAction extends AxiosAction<LoadInvitationsType> {}

//---------------------------------------------------------------------------------------------------

type LoadInvitationType = typeof LOAD_INVITATION | typeof LOAD_INVITATION_SUCCESS | typeof LOAD_INVITATION_FAIL;

interface LoadInvitationActionCreator extends AxiosActionCreator<LoadInvitationType> {}

interface LoadInvitationAction extends AxiosAction<LoadInvitationType> {}

//---------------------------------------------------------------------------------------------------

type AddInvitationType = typeof ADD_INVITATION | typeof ADD_INVITATION_SUCCESS | typeof ADD_INVITATION_FAIL;

interface AddInvitationActionCreator extends AxiosActionCreator<AddInvitationType> {}

interface AddInvitationAction extends AxiosAction<AddInvitationType> {}

//---------------------------------------------------------------------------------------------------

type AcceptInvitationType = typeof ACCEPT_INVITATION | typeof ACCEPT_INVITATION_SUCCESS | typeof ACCEPT_INVITATION_FAIL;

interface AcceptInvitationActionCreator extends AxiosActionCreator<AcceptInvitationType> {}

interface AcceptInvitationAction extends AxiosAction<AcceptInvitationType> {}

//---------------------------------------------------------------------------------------------------

type DeclineInvitationType = typeof DECLINE_INVITATION | typeof DECLINE_INVITATION_SUCCESS | typeof DECLINE_INVITATION_FAIL;

interface DeclineInvitationActionCreator extends AxiosActionCreator<DeclineInvitationType> {}

interface DeclineInvitationAction extends AxiosAction<DeclineInvitationType> {}

//---------------------------------------------------------------------------------------------------

export type InvitationsActionCreators = LoadInvitationsActionCreator | LoadInvitationActionCreator | AddInvitationActionCreator | AcceptInvitationActionCreator | DeclineInvitationActionCreator;

export type InvitationsActionTypes = LoadInvitationsAction | LoadInvitationAction | AddInvitationAction | AcceptInvitationAction | DeclineInvitationAction;

export interface InvitationsState {}

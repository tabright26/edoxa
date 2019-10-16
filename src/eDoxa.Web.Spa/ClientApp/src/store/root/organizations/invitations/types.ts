import { AxiosActionCreator, AxiosAction, AxiosState } from "store/middlewares/axios/types";

export const LOAD_CLAN_INVITATIONS = "LOAD_CLAN_INVITATIONS";
export const LOAD_CLAN_INVITATIONS_SUCCESS = "LOAD_CLAN_INVITATIONS_SUCCESS";
export const LOAD_CLAN_INVITATIONS_FAIL = "LOAD_CLAN_INVITATIONS_FAIL";

export const LOAD_CLAN_INVITATION = "LOAD_CLAN_INVITATION";
export const LOAD_CLAN_INVITATION_SUCCESS = "LOAD_CLAN_INVITATION_SUCCESS";
export const LOAD_CLAN_INVITATION_FAIL = "LOAD_CLAN_INVITATION_FAIL";

export const SEND_CLAN_INVITATION = "SEND_CLAN_INVITATION";
export const SEND_CLAN_INVITATION_SUCCESS = "SEND_CLAN_INVITATION_SUCCESS";
export const SEND_CLAN_INVITATION_FAIL = "SEND_CLAN_INVITATION_FAIL";

export const ACCEPT_CLAN_INVITATION = "ACCEPT_CLAN_INVITATION";
export const ACCEPT_CLAN_INVITATION_SUCCESS = "ACCEPT_CLAN_INVITATION_SUCCESS";
export const ACCEPT_CLAN_INVITATION_FAIL = "ACCEPT_CLAN_INVITATION_FAIL";

export const DECLINE_CLAN_INVITATION = "DECLINE_CLAN_INVITATION";
export const DECLINE_CLAN_INVITATION_SUCCESS = "DECLINE_CLAN_INVITATION_SUCCESS";
export const DECLINE_CLAN_INVITATION_FAIL = "DECLINE_CLAN_INVITATION_FAIL";

type LoadClanInvitationsType = typeof LOAD_CLAN_INVITATIONS | typeof LOAD_CLAN_INVITATIONS_SUCCESS | typeof LOAD_CLAN_INVITATIONS_FAIL;

interface LoadClanInvitationsActionCreator extends AxiosActionCreator<LoadClanInvitationsType> {}

interface LoadClanInvitationsAction extends AxiosAction<LoadClanInvitationsType> {}

type LoadClanInvitationType = typeof LOAD_CLAN_INVITATION | typeof LOAD_CLAN_INVITATION_SUCCESS | typeof LOAD_CLAN_INVITATION_FAIL;

interface LoadClanInvitationActionCreator extends AxiosActionCreator<LoadClanInvitationType> {}

interface LoadClanInvitationAction extends AxiosAction<LoadClanInvitationType> {}

type SendClanInvitationType = typeof SEND_CLAN_INVITATION | typeof SEND_CLAN_INVITATION_SUCCESS | typeof SEND_CLAN_INVITATION_FAIL;

interface SendClanInvitationActionCreator extends AxiosActionCreator<SendClanInvitationType> {}

interface SendClanInvitationAction extends AxiosAction<SendClanInvitationType> {}

type AcceptClanInvitationType = typeof ACCEPT_CLAN_INVITATION | typeof ACCEPT_CLAN_INVITATION_SUCCESS | typeof ACCEPT_CLAN_INVITATION_FAIL;

interface AcceptClanInvitationActionCreator extends AxiosActionCreator<AcceptClanInvitationType> {}

interface AcceptClanInvitationAction extends AxiosAction<AcceptClanInvitationType> {}

type DeclineClanInvitationType = typeof DECLINE_CLAN_INVITATION | typeof DECLINE_CLAN_INVITATION_SUCCESS | typeof DECLINE_CLAN_INVITATION_FAIL;

interface DeclineClanInvitationActionCreator extends AxiosActionCreator<DeclineClanInvitationType> {}

interface DeclineClanInvitationAction extends AxiosAction<DeclineClanInvitationType> {}

export type ClanInvitationsActionCreators =
  | LoadClanInvitationsActionCreator
  | LoadClanInvitationActionCreator
  | SendClanInvitationActionCreator
  | AcceptClanInvitationActionCreator
  | DeclineClanInvitationActionCreator;

export type ClanInvitationsActions = LoadClanInvitationsAction | LoadClanInvitationAction | SendClanInvitationAction | AcceptClanInvitationAction | DeclineClanInvitationAction;

export type ClanInvitationsState = AxiosState;

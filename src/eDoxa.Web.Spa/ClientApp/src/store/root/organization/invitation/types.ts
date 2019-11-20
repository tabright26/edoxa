import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";

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

export type LoadClanInvitationsType = typeof LOAD_CLAN_INVITATIONS | typeof LOAD_CLAN_INVITATIONS_SUCCESS | typeof LOAD_CLAN_INVITATIONS_FAIL;
export type LoadClanInvitationsActionCreator = AxiosActionCreator<LoadClanInvitationsType>;
export type LoadClanInvitationsAction = AxiosAction<LoadClanInvitationsType>;

export type LoadClanInvitationType = typeof LOAD_CLAN_INVITATION | typeof LOAD_CLAN_INVITATION_SUCCESS | typeof LOAD_CLAN_INVITATION_FAIL;
export type LoadClanInvitationActionCreator = AxiosActionCreator<LoadClanInvitationType>;
export type LoadClanInvitationAction = AxiosAction<LoadClanInvitationType>;

export type SendClanInvitationType = typeof SEND_CLAN_INVITATION | typeof SEND_CLAN_INVITATION_SUCCESS | typeof SEND_CLAN_INVITATION_FAIL;
export type SendClanInvitationActionCreator = AxiosActionCreator<SendClanInvitationType>;
export type SendClanInvitationAction = AxiosAction<SendClanInvitationType>;

export type AcceptClanInvitationType = typeof ACCEPT_CLAN_INVITATION | typeof ACCEPT_CLAN_INVITATION_SUCCESS | typeof ACCEPT_CLAN_INVITATION_FAIL;
export type AcceptClanInvitationActionCreator = AxiosActionCreator<AcceptClanInvitationType>;
export type AcceptClanInvitationAction = AxiosAction<AcceptClanInvitationType>;

export type DeclineClanInvitationType = typeof DECLINE_CLAN_INVITATION | typeof DECLINE_CLAN_INVITATION_SUCCESS | typeof DECLINE_CLAN_INVITATION_FAIL;
export type DeclineClanInvitationActionCreator = AxiosActionCreator<DeclineClanInvitationType>;
export type DeclineClanInvitationAction = AxiosAction<DeclineClanInvitationType>;

export type ClanInvitationsActionCreators =
  | LoadClanInvitationsActionCreator
  | LoadClanInvitationActionCreator
  | SendClanInvitationActionCreator
  | AcceptClanInvitationActionCreator
  | DeclineClanInvitationActionCreator;
export type ClanInvitationsActions = LoadClanInvitationsAction | LoadClanInvitationAction | SendClanInvitationAction | AcceptClanInvitationAction | DeclineClanInvitationAction;
export type ClanInvitationsState = AxiosState;

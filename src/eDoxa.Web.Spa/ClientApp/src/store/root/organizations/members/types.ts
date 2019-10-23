import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";

export const LOAD_CLAN_MEMBERS = "LOAD_CLAN_MEMBERS";
export const LOAD_CLAN_MEMBERS_SUCCESS = "LOAD_CLAN_MEMBERS_SUCCESS";
export const LOAD_CLAN_MEMBERS_FAIL = "LOAD_CLAN_MEMBERS_FAIL";

export const KICK_CLAN_MEMBER = "KICK_CLAN_MEMBER";
export const KICK_CLAN_MEMBER_SUCCESS = "KICK_CLAN_MEMBER_SUCCESS";
export const KICK_CLAN_MEMBER_FAIL = "KICK_CLAN_MEMBER_FAIL";

export type LoadClanMembersType = typeof LOAD_CLAN_MEMBERS | typeof LOAD_CLAN_MEMBERS_SUCCESS | typeof LOAD_CLAN_MEMBERS_FAIL;
export type LoadClanMembersActionCreator = AxiosActionCreator<LoadClanMembersType>;
export type LoadClanMembersAction = AxiosAction<LoadClanMembersType>;

export type KickClanMemberType = typeof KICK_CLAN_MEMBER | typeof KICK_CLAN_MEMBER_SUCCESS | typeof KICK_CLAN_MEMBER_FAIL;
export type KickClanMemberActionCreator = AxiosActionCreator<KickClanMemberType>;
export type KickClanMemberAction = AxiosAction<KickClanMemberType>;

export type ClanMembersActionCreators = LoadClanMembersActionCreator | KickClanMemberActionCreator;
export type ClanMembersActions = LoadClanMembersAction | KickClanMemberAction;
export type ClanMembersState = AxiosState;

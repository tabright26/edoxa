import { AxiosActionCreator, AxiosAction } from "store/types";

export const LOAD_MEMBERS = "LOAD_MEMBERS";
export const LOAD_MEMBERS_SUCCESS = "LOAD_MEMBERS_SUCCESS";
export const LOAD_MEMBERS_FAIL = "LOAD_MEMBERS_FAIL";

export const KICK_MEMBER = "KICK_MEMBER";
export const KICK_MEMBER_SUCCESS = "KICK_MEMBER_SUCCESS";
export const KICK_MEMBER_FAIL = "KICK_MEMBER_FAIL";

export const LEAVE_CLAN = "LEAVE_CLAN";
export const LEAVE_CLAN_SUCCESS = "LEAVE_CLAN_SUCCESS";
export const LEAVE_CLAN_FAIL = "LEAVE_CLAN_FAIL";

type LoadMembersType = typeof LOAD_MEMBERS | typeof LOAD_MEMBERS_SUCCESS | typeof LOAD_MEMBERS_FAIL;

interface LoadMembersActionCreator extends AxiosActionCreator<LoadMembersType> {}

interface LoadMembersAction extends AxiosAction<LoadMembersType> {}

//---------------------------------------------------------------------------------------------------------

type KickMemberType = typeof KICK_MEMBER | typeof KICK_MEMBER_SUCCESS | typeof KICK_MEMBER_FAIL;

interface KickMemberActionCreator extends AxiosActionCreator<KickMemberType> {}

interface KickMemberAction extends AxiosAction<KickMemberType> {}

//---------------------------------------------------------------------------------------------------------

type LeaveClanType = typeof LEAVE_CLAN | typeof LEAVE_CLAN_SUCCESS | typeof LEAVE_CLAN_FAIL;

interface LeaveClanActionCreator extends AxiosActionCreator<LeaveClanType> {}

interface LeaveClanAction extends AxiosAction<LeaveClanType> {}

//---------------------------------------------------------------------------------------------------------

export type MembersActionCreators = LoadMembersActionCreator | KickMemberActionCreator | LeaveClanActionCreator;

export type MembersActionTypes = LoadMembersAction | KickMemberAction | LeaveClanAction;

export interface MembersState {}

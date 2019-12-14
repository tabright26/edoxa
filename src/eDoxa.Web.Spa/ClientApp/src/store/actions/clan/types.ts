import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";
import { Candidature, Invitation, Member, Clan } from "types";

//------------------------------------------------------------------------------------------------------------

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
export const ACCEPT_CLAN_CANDIDATURE_SUCCESS =
  "ACCEPT_CLAN_CANDIDATURE_SUCCESS";
export const ACCEPT_CLAN_CANDIDATURE_FAIL = "ACCEPT_CLAN_CANDIDATURE_FAIL";

export const DECLINE_CLAN_CANDIDATURE = "REFUSE_CLAN_CANDIDATURE";
export const DECLINE_CLAN_CANDIDATURE_SUCCESS =
  "REFUSE_CLAN_CANDIDATURE_SUCCESS";
export const DECLINE_CLAN_CANDIDATURE_FAIL = "REFUSE_CLAN_CANDIDATURE_FAIL";

export type LoadClanCandidaturesdType =
  | typeof LOAD_CLAN_CANDIDATURES
  | typeof LOAD_CLAN_CANDIDATURES_SUCCESS
  | typeof LOAD_CLAN_CANDIDATURES_FAIL;

export type LoadClanCandidaturesActionCreator = AxiosActionCreator<
  LoadClanCandidaturesdType
>;

export type LoadClanCandidaturesAction = AxiosAction<
  LoadClanCandidaturesdType,
  Candidature[]
>;

export type LoadClanCandidatureType =
  | typeof LOAD_CLAN_CANDIDATURE
  | typeof LOAD_CLAN_CANDIDATURE_SUCCESS
  | typeof LOAD_CLAN_CANDIDATURE_FAIL;

export type LoadClanCandidatureActionCreator = AxiosActionCreator<
  LoadClanCandidatureType
>;

export type LoadClanCandidatureAction = AxiosAction<
  LoadClanCandidatureType,
  Candidature
>;

export type SendClanCandidatureType =
  | typeof SEND_CLAN_CANDIDATURE
  | typeof SEND_CLAN_CANDIDATURE_SUCCESS
  | typeof SEND_CLAN_CANDIDATURE_FAIL;

export type SendClanCandidatureActionCreator = AxiosActionCreator<
  SendClanCandidatureType
>;

export type SendClanCandidatureAction = AxiosAction<
  SendClanCandidatureType,
  Candidature
>;

export type AcceptClanCandidatureType =
  | typeof ACCEPT_CLAN_CANDIDATURE
  | typeof ACCEPT_CLAN_CANDIDATURE_SUCCESS
  | typeof ACCEPT_CLAN_CANDIDATURE_FAIL;

export type AcceptClanCandidatureActionCreator = AxiosActionCreator<
  AcceptClanCandidatureType
>;

export type AcceptClanCandidatureAction = AxiosAction<
  AcceptClanCandidatureType,
  Candidature
>;

export type RefuseClanCandidatureType =
  | typeof DECLINE_CLAN_CANDIDATURE
  | typeof DECLINE_CLAN_CANDIDATURE_SUCCESS
  | typeof DECLINE_CLAN_CANDIDATURE_FAIL;

export type RefuseClanCandidatureActionCreator = AxiosActionCreator<
  RefuseClanCandidatureType
>;

export type RefuseClanCandidatureAction = AxiosAction<
  RefuseClanCandidatureType,
  Candidature
>;

export type ClanCandidaturesActionCreators =
  | LoadClanCandidaturesActionCreator
  | LoadClanCandidatureActionCreator
  | SendClanCandidatureActionCreator
  | AcceptClanCandidatureActionCreator
  | RefuseClanCandidatureActionCreator;

export type ClanCandidaturesActions =
  | LoadClanCandidaturesAction
  | LoadClanCandidatureAction
  | SendClanCandidatureAction
  | AcceptClanCandidatureAction
  | RefuseClanCandidatureAction;

//------------------------------------------------------------------------------------------------------------

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
export const DECLINE_CLAN_INVITATION_SUCCESS =
  "DECLINE_CLAN_INVITATION_SUCCESS";
export const DECLINE_CLAN_INVITATION_FAIL = "DECLINE_CLAN_INVITATION_FAIL";

export type LoadClanInvitationsdType =
  | typeof LOAD_CLAN_INVITATIONS
  | typeof LOAD_CLAN_INVITATIONS_SUCCESS
  | typeof LOAD_CLAN_INVITATIONS_FAIL;

export type LoadClanInvitationsActionCreator = AxiosActionCreator<
  LoadClanInvitationsdType
>;

export type LoadClanInvitationsAction = AxiosAction<
  LoadClanInvitationsdType,
  Invitation[]
>;

export type LoadClanInvitationType =
  | typeof LOAD_CLAN_INVITATION
  | typeof LOAD_CLAN_INVITATION_SUCCESS
  | typeof LOAD_CLAN_INVITATION_FAIL;

export type LoadClanInvitationActionCreator = AxiosActionCreator<
  LoadClanInvitationType
>;

export type LoadClanInvitationAction = AxiosAction<
  LoadClanInvitationType,
  Invitation
>;

export type SendClanInvitationType =
  | typeof SEND_CLAN_INVITATION
  | typeof SEND_CLAN_INVITATION_SUCCESS
  | typeof SEND_CLAN_INVITATION_FAIL;

export type SendClanInvitationActionCreator = AxiosActionCreator<
  SendClanInvitationType
>;

export type SendClanInvitationAction = AxiosAction<
  SendClanInvitationType,
  Invitation
>;

export type AcceptClanInvitationType =
  | typeof ACCEPT_CLAN_INVITATION
  | typeof ACCEPT_CLAN_INVITATION_SUCCESS
  | typeof ACCEPT_CLAN_INVITATION_FAIL;

export type AcceptClanInvitationActionCreator = AxiosActionCreator<
  AcceptClanInvitationType
>;

export type AcceptClanInvitationAction = AxiosAction<
  AcceptClanInvitationType,
  Invitation
>;

export type RefuseClanInvitationType =
  | typeof DECLINE_CLAN_INVITATION
  | typeof DECLINE_CLAN_INVITATION_SUCCESS
  | typeof DECLINE_CLAN_INVITATION_FAIL;

export type RefuseClanInvitationActionCreator = AxiosActionCreator<
  RefuseClanInvitationType
>;

export type RefuseClanInvitationAction = AxiosAction<
  RefuseClanInvitationType,
  Invitation
>;

export type ClanInvitationsActionCreators =
  | LoadClanInvitationsActionCreator
  | LoadClanInvitationActionCreator
  | SendClanInvitationActionCreator
  | AcceptClanInvitationActionCreator
  | RefuseClanInvitationActionCreator;

export type ClanInvitationsActions =
  | LoadClanInvitationsAction
  | LoadClanInvitationAction
  | SendClanInvitationAction
  | AcceptClanInvitationAction
  | RefuseClanInvitationAction;

//------------------------------------------------------------------------------------------------------------

export const LOAD_CLANS = "LOAD_CLANS";
export const LOAD_CLANS_SUCCESS = "LOAD_CLANS_SUCCESS";
export const LOAD_CLANS_FAIL = "LOAD_CLANS_FAIL";

export const LOAD_CLAN = "LOAD_CLAN";
export const LOAD_CLAN_SUCCESS = "LOAD_CLAN_SUCCESS";
export const LOAD_CLAN_FAIL = "LOAD_CLAN_FAIL";

export const CREATE_CLAN = "CREATE_CLAN";
export const CREATE_CLAN_SUCCESS = "CREATE_CLAN_SUCCESS";
export const CREATE_CLAN_FAIL = "CREATE_CLAN_FAIL";

export const LEAVE_CLAN = "LEAVE_CLAN";
export const LEAVE_CLAN_SUCCESS = "LEAVE_CLAN_SUCCESS";
export const LEAVE_CLAN_FAIL = "LEAVE_CLAN_FAIL";

export type LoadClansType =
  | typeof LOAD_CLANS
  | typeof LOAD_CLANS_SUCCESS
  | typeof LOAD_CLANS_FAIL;
export type LoadClansActionCreator = AxiosActionCreator<LoadClansType>;
export type LoadClansAction = AxiosAction<LoadClansType, Clan[]>;

export type LoadClanType =
  | typeof LOAD_CLAN
  | typeof LOAD_CLAN_SUCCESS
  | typeof LOAD_CLAN_FAIL;
export type LoadClanActionCreator = AxiosActionCreator<LoadClanType>;
export type LoadClanAction = AxiosAction<LoadClanType, Clan>;

export type CreateClanType =
  | typeof CREATE_CLAN
  | typeof CREATE_CLAN_SUCCESS
  | typeof CREATE_CLAN_FAIL;
export type CreateClanActionCreator = AxiosActionCreator<CreateClanType>;
export type CreateClanAction = AxiosAction<CreateClanType, Clan>;

export type LeaveClanType =
  | typeof LEAVE_CLAN
  | typeof LEAVE_CLAN_SUCCESS
  | typeof LEAVE_CLAN_FAIL;
export type LeaveClanActionCreator = AxiosActionCreator<LeaveClanType>;
export type LeaveClanAction = AxiosAction<LeaveClanType>;

export type ClansActionCreators =
  | LoadClansActionCreator
  | LoadClanActionCreator
  | CreateClanActionCreator
  | LeaveClanActionCreator;
export type ClansActions =
  | LoadClansAction
  | LoadClanAction
  | CreateClanAction
  | LeaveClanAction;
export type ClansState = AxiosState<Clan[]>;

//------------------------------------------------------------------------------------------------------------

export const DOWNLOAD_CLAN_LOGO = "DOWNLOAD_CLAN_LOGO";
export const DOWNLOAD_CLAN_LOGO_SUCCESS = "DOWNLOAD_CLAN_LOGO_SUCCESS";
export const DOWNLOAD_CLAN_LOGO_FAIL = "DOWNLOAD_CLAN_LOGO_FAIL";

export const UPLOAD_CLAN_LOGO = "UPLOAD_CLAN_LOGO";
export const UPLOAD_CLAN_LOGO_SUCCESS = "UPLOAD_CLAN_LOGO_SUCCESS";
export const UPLOAD_CLAN_LOGO_FAIL = "UPLOAD_CLAN_LOGO_FAIL";

export type DownloadClanLogoType =
  | typeof DOWNLOAD_CLAN_LOGO
  | typeof DOWNLOAD_CLAN_LOGO_SUCCESS
  | typeof DOWNLOAD_CLAN_LOGO_FAIL;
export type DownloadClanLogoActionCreator = AxiosActionCreator<
  DownloadClanLogoType
>;
export type DownloadClanLogoAction = AxiosAction<DownloadClanLogoType>;

export type UploadClanLogoType =
  | typeof UPLOAD_CLAN_LOGO
  | typeof UPLOAD_CLAN_LOGO_SUCCESS
  | typeof UPLOAD_CLAN_LOGO_FAIL;
export type UploadClanLogoActionCreator = AxiosActionCreator<
  UploadClanLogoType
>;
export type UploadClanLogoAction = AxiosAction<UploadClanLogoType>;

export type ClanLogosActionCreators =
  | DownloadClanLogoActionCreator
  | UploadClanLogoActionCreator;
export type ClanLogosActions = DownloadClanLogoAction | UploadClanLogoAction;

//------------------------------------------------------------------------------------------------------------

export const LOAD_CLAN_MEMBERS = "LOAD_CLAN_MEMBERS";
export const LOAD_CLAN_MEMBERS_SUCCESS = "LOAD_CLAN_MEMBERS_SUCCESS";
export const LOAD_CLAN_MEMBERS_FAIL = "LOAD_CLAN_MEMBERS_FAIL";

export const KICK_CLAN_MEMBER = "KICK_CLAN_MEMBER";
export const KICK_CLAN_MEMBER_SUCCESS = "KICK_CLAN_MEMBER_SUCCESS";
export const KICK_CLAN_MEMBER_FAIL = "KICK_CLAN_MEMBER_FAIL";

export type LoadClanMembersType =
  | typeof LOAD_CLAN_MEMBERS
  | typeof LOAD_CLAN_MEMBERS_SUCCESS
  | typeof LOAD_CLAN_MEMBERS_FAIL;
export type LoadClanMembersActionCreator = AxiosActionCreator<
  LoadClanMembersType
>;
export type LoadClanMembersAction = AxiosAction<LoadClanMembersType, Member[]>;

export type KickClanMemberType =
  | typeof KICK_CLAN_MEMBER
  | typeof KICK_CLAN_MEMBER_SUCCESS
  | typeof KICK_CLAN_MEMBER_FAIL;
export type KickClanMemberActionCreator = AxiosActionCreator<
  KickClanMemberType
>;
export type KickClanMemberAction = AxiosAction<KickClanMemberType, Member>;

export type ClanMembersActionCreators =
  | LoadClanMembersActionCreator
  | KickClanMemberActionCreator;
export type ClanMembersActions = LoadClanMembersAction | KickClanMemberAction;

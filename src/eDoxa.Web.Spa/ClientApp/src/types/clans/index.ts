import { UserDoxatag, UserId } from "types/identity";

export type ClanId = string;
export type MemberId = string;
export type CandidatureId = string;
export type InvitationId = string;

export interface Clan {
  readonly id: ClanId;
  readonly name: string;
  readonly ownerId: UserId;
  readonly owner?: ClanOwner;
  readonly members: ClanMember[];
  readonly logo: ClanLogo;
}

export type ClanLogo = string;

export interface ClanOwner {
  readonly userId: UserId;
  readonly doxatag?: UserDoxatag;
}

export interface ClanMember {
  readonly id: MemberId;
  readonly clanId: ClanId;
  readonly userId: UserId;
  readonly doxatag?: UserDoxatag;
}

export interface ClanCandidature {
  readonly id: CandidatureId;
  readonly clanId: ClanId;
  readonly userId: UserId;
  readonly doxatag?: UserDoxatag;
  readonly clan?: Clan;
}

export interface ClanInvitation {
  readonly id: InvitationId;
  readonly clanId: ClanId;
  readonly userId: UserId;
  readonly doxatag?: UserDoxatag;
  readonly clan?: Clan;
}

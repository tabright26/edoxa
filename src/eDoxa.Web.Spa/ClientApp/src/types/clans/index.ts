import { Doxatag, UserId } from "types/identity";

export type ClanId = string;
export type ClanMemberId = string;
export type ClanName = string;
export type ClanOwner = Doxatag;
export type ClanLogo = string;
export type CandidatureId = string;
export type InvitationId = string;

export interface Clan {
  readonly id: ClanId;
  readonly name: ClanName;
  readonly owner: ClanOwner;
  readonly members: ClanMember[];
  readonly logo: ClanLogo;
}

export interface ClanMember {
  readonly id: ClanMemberId;
  readonly clanId: ClanId;
  readonly userId: UserId;
  readonly doxatag: Doxatag;
}

export interface Candidature {
  readonly id: CandidatureId;
  readonly clanId: ClanId;
  readonly userId: UserId;
  readonly doxatag: Doxatag;
  readonly clan?: Clan;
}

export interface Invitation {
  readonly id: InvitationId;
  readonly clanId: ClanId;
  readonly userId: UserId;
  readonly doxatag: Doxatag;
  readonly clan?: Clan;
}

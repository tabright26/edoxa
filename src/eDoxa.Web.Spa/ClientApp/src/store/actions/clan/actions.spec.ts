import {
  loadClanCandidatures,
  loadClanCandidature,
  sendClanCandidature,
  acceptClanCandidature,
  declineClanCandidature,
  loadClanMembers,
  kickClanMember,
  loadClans,
  loadClan,
  createClan,
  leaveClan,
  loadClanInvitations,
  loadClanInvitation,
  sendClanInvitation,
  acceptClanInvitation,
  declineClanInvitation,
  downloadClanLogo,
  uploadClanLogo
} from "./actions";

import {
  LOAD_CLAN_CANDIDATURES,
  LOAD_CLAN_CANDIDATURES_SUCCESS,
  LOAD_CLAN_CANDIDATURES_FAIL,
  LOAD_CLAN_CANDIDATURE,
  LOAD_CLAN_CANDIDATURE_SUCCESS,
  LOAD_CLAN_CANDIDATURE_FAIL,
  SEND_CLAN_CANDIDATURE,
  SEND_CLAN_CANDIDATURE_SUCCESS,
  SEND_CLAN_CANDIDATURE_FAIL,
  ACCEPT_CLAN_CANDIDATURE,
  ACCEPT_CLAN_CANDIDATURE_SUCCESS,
  ACCEPT_CLAN_CANDIDATURE_FAIL,
  DECLINE_CLAN_CANDIDATURE,
  DECLINE_CLAN_CANDIDATURE_SUCCESS,
  DECLINE_CLAN_CANDIDATURE_FAIL,
  LOAD_CLAN_MEMBERS,
  LOAD_CLAN_MEMBERS_SUCCESS,
  LOAD_CLAN_MEMBERS_FAIL,
  KICK_CLAN_MEMBER,
  KICK_CLAN_MEMBER_SUCCESS,
  KICK_CLAN_MEMBER_FAIL,
  LOAD_CLANS,
  LOAD_CLANS_SUCCESS,
  LOAD_CLANS_FAIL,
  LOAD_CLAN,
  LOAD_CLAN_SUCCESS,
  LOAD_CLAN_FAIL,
  CREATE_CLAN,
  CREATE_CLAN_SUCCESS,
  CREATE_CLAN_FAIL,
  LEAVE_CLAN,
  LEAVE_CLAN_FAIL,
  LEAVE_CLAN_SUCCESS,
  LOAD_CLAN_INVITATIONS,
  LOAD_CLAN_INVITATIONS_SUCCESS,
  LOAD_CLAN_INVITATIONS_FAIL,
  LOAD_CLAN_INVITATION,
  LOAD_CLAN_INVITATION_SUCCESS,
  LOAD_CLAN_INVITATION_FAIL,
  SEND_CLAN_INVITATION,
  SEND_CLAN_INVITATION_SUCCESS,
  SEND_CLAN_INVITATION_FAIL,
  ACCEPT_CLAN_INVITATION,
  ACCEPT_CLAN_INVITATION_SUCCESS,
  ACCEPT_CLAN_INVITATION_FAIL,
  DECLINE_CLAN_INVITATION,
  DECLINE_CLAN_INVITATION_SUCCESS,
  DECLINE_CLAN_INVITATION_FAIL,
  DOWNLOAD_CLAN_LOGO,
  DOWNLOAD_CLAN_LOGO_SUCCESS,
  DOWNLOAD_CLAN_LOGO_FAIL,
  UPLOAD_CLAN_LOGO,
  UPLOAD_CLAN_LOGO_SUCCESS,
  UPLOAD_CLAN_LOGO_FAIL
} from "./types";

describe("candidatures", () => {
  it("should create an action to get user candidatures", () => {
    const type = "user";
    const id = "0";

    const expectedType = [
      LOAD_CLAN_CANDIDATURES,
      LOAD_CLAN_CANDIDATURES_SUCCESS,
      LOAD_CLAN_CANDIDATURES_FAIL
    ];
    const expectedMethod = "GET";
    const expectedUrl = `/clans/api/candidatures?${type}Id=${id}`;

    const actionCreator = loadClanCandidatures(type, id);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get clan candidatures", () => {
    const type = "clan";
    const id = "100";

    const expectedType = [
      LOAD_CLAN_CANDIDATURES,
      LOAD_CLAN_CANDIDATURES_SUCCESS,
      LOAD_CLAN_CANDIDATURES_FAIL
    ];
    const expectedMethod = "GET";
    const expectedUrl = `/clans/api/candidatures?${type}Id=${id}`;

    const actionCreator = loadClanCandidatures(type, id);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get a specific candidature", () => {
    const candidatureId = "10";

    const expectedType = [
      LOAD_CLAN_CANDIDATURE,
      LOAD_CLAN_CANDIDATURE_SUCCESS,
      LOAD_CLAN_CANDIDATURE_FAIL
    ];
    const expectedMethod = "GET";
    const expectedUrl = `/organizations/clans/api/candidatures/${candidatureId}`;

    const actionCreator = loadClanCandidature(candidatureId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to add a candidature", () => {
    const clanId = "10";
    const userId = "10";

    const expectedType = [
      SEND_CLAN_CANDIDATURE,
      SEND_CLAN_CANDIDATURE_SUCCESS,
      SEND_CLAN_CANDIDATURE_FAIL
    ];
    const expectedMethod = "POST";
    const expectedUrl = "/organizations/clans/api/candidatures";

    const actionCreator = sendClanCandidature(clanId, userId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
    expect(actionCreator.payload.request.data).toEqual({
      clanId: clanId,
      userId: userId
    });
  });

  it("should create an action to accept a candidature", () => {
    const candidatureId = "10";

    const expectedType = [
      ACCEPT_CLAN_CANDIDATURE,
      ACCEPT_CLAN_CANDIDATURE_SUCCESS,
      ACCEPT_CLAN_CANDIDATURE_FAIL
    ];
    const expectedMethod = "POST";
    const expectedUrl = `/organizations/clans/api/candidatures/${candidatureId}`;

    const actionCreator = acceptClanCandidature(candidatureId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to decline a candidature", () => {
    const candidatureId = "10";

    const expectedType = [
      DECLINE_CLAN_CANDIDATURE,
      DECLINE_CLAN_CANDIDATURE_SUCCESS,
      DECLINE_CLAN_CANDIDATURE_FAIL
    ];
    const expectedMethod = "DELETE";
    const expectedUrl = `/organizations/clans/api/candidatures/${candidatureId}`;

    const actionCreator = declineClanCandidature(candidatureId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});

describe("clans", () => {
  it("should create an action to get all clans", () => {
    const expectedType = [LOAD_CLANS, LOAD_CLANS_SUCCESS, LOAD_CLANS_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = "/clans/api/clans";

    const actionCreator = loadClans();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get a specific clan", () => {
    const clanId = "0";

    const expectedType = [LOAD_CLAN, LOAD_CLAN_SUCCESS, LOAD_CLAN_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = `/clans/api/clans/${clanId}`;

    const actionCreator = loadClan(clanId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to create a clan", () => {
    const data = { name: "clanName" };

    const expectedType = [CREATE_CLAN, CREATE_CLAN_SUCCESS, CREATE_CLAN_FAIL];
    const expectedMethod = "POST";
    const expectedUrl = "/organizations/clans/api/clans";

    const actionCreator = createClan(data);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
    expect(actionCreator.payload.request.data).toEqual(data);
  });

  it("should create an action to leave the clan", () => {
    const clanId = "0";

    const expectedType = [LEAVE_CLAN, LEAVE_CLAN_SUCCESS, LEAVE_CLAN_FAIL];
    const expectedMethod = "DELETE";
    const expectedUrl = `/organizations/clans/api/clans/${clanId}/members`;

    const actionCreator = leaveClan(clanId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});

describe("invitations", () => {
  it("should create an action to get user invitations", () => {
    const type = "user";
    const id = "0";

    const expectedType = [
      LOAD_CLAN_INVITATIONS,
      LOAD_CLAN_INVITATIONS_SUCCESS,
      LOAD_CLAN_INVITATIONS_FAIL
    ];
    const expectedMethod = "GET";
    const expectedUrl = `/clans/api/invitations?${type}Id=${id}`;

    const actionCreator = loadClanInvitations(type, id);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get clan invitations", () => {
    const type = "clan";
    const id = "100";

    const expectedType = [
      LOAD_CLAN_INVITATIONS,
      LOAD_CLAN_INVITATIONS_SUCCESS,
      LOAD_CLAN_INVITATIONS_FAIL
    ];
    const expectedMethod = "GET";
    const expectedUrl = `/clans/api/invitations?${type}Id=${id}`;

    const actionCreator = loadClanInvitations(type, id);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get a specific invitation", () => {
    const invitationId = "10";

    const expectedType = [
      LOAD_CLAN_INVITATION,
      LOAD_CLAN_INVITATION_SUCCESS,
      LOAD_CLAN_INVITATION_FAIL
    ];
    const expectedMethod = "GET";
    const expectedUrl = `/clans/api/invitations/${invitationId}`;

    const actionCreator = loadClanInvitation(invitationId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to add a invitation", () => {
    const clanId = "10";
    const userId = "10";

    const expectedType = [
      SEND_CLAN_INVITATION,
      SEND_CLAN_INVITATION_SUCCESS,
      SEND_CLAN_INVITATION_FAIL
    ];
    const expectedMethod = "POST";
    const expectedUrl = "/clans/api/invitations";

    const actionCreator = sendClanInvitation(clanId, userId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
    expect(actionCreator.payload.request.data).toEqual({
      clanId: clanId,
      userId: userId
    });
  });

  it("should create an action to accept a invitation", () => {
    const invitationId = "10";

    const expectedType = [
      ACCEPT_CLAN_INVITATION,
      ACCEPT_CLAN_INVITATION_SUCCESS,
      ACCEPT_CLAN_INVITATION_FAIL
    ];
    const expectedMethod = "POST";
    const expectedUrl = `/clans/api/invitations/${invitationId}`;

    const actionCreator = acceptClanInvitation(invitationId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to decline a invitation", () => {
    const invitationId = "10";

    const expectedType = [
      DECLINE_CLAN_INVITATION,
      DECLINE_CLAN_INVITATION_SUCCESS,
      DECLINE_CLAN_INVITATION_FAIL
    ];
    const expectedMethod = "DELETE";
    const expectedUrl = `/clans/api/invitations/${invitationId}`;

    const actionCreator = declineClanInvitation(invitationId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});

describe("clans", () => {
  it("should create an action to get a specific clan photo", () => {
    const clanId = "0";

    const expectedType = [
      DOWNLOAD_CLAN_LOGO,
      DOWNLOAD_CLAN_LOGO_SUCCESS,
      DOWNLOAD_CLAN_LOGO_FAIL
    ];
    const expectedMethod = "GET";
    const expectedUrl = `/clans/api/clans/${clanId}/logo`;

    const actionCreator = downloadClanLogo(clanId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to update a clan logo", () => {
    const clanId = "0";
    const data = { logo: "data" };

    const expectedType = [
      UPLOAD_CLAN_LOGO,
      UPLOAD_CLAN_LOGO_SUCCESS,
      UPLOAD_CLAN_LOGO_FAIL
    ];
    const expectedMethod = "POST";
    const expectedUrl = `/clans/api/clans/${clanId}/logo`;

    const actionCreator = uploadClanLogo(clanId, data);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
    expect(actionCreator.payload.request.data).toEqual(data);
  });
});

describe("members", () => {
  it("should create an action to get a specific clan members", () => {
    const clanId = "0";

    const expectedType = [
      LOAD_CLAN_MEMBERS,
      LOAD_CLAN_MEMBERS_SUCCESS,
      LOAD_CLAN_MEMBERS_FAIL
    ];
    const expectedMethod = "GET";
    const expectedUrl = `/clans/api/clans/${clanId}/members`;

    const actionCreator = loadClanMembers(clanId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to kick a member", () => {
    const clanId = "0";
    const memberId = "10";

    const expectedType = [
      KICK_CLAN_MEMBER,
      KICK_CLAN_MEMBER_SUCCESS,
      KICK_CLAN_MEMBER_FAIL
    ];
    const expectedMethod = "DELETE";
    const expectedUrl = `/clans/api/clans/${clanId}/members/${memberId}`;

    const actionCreator = kickClanMember(clanId, memberId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});

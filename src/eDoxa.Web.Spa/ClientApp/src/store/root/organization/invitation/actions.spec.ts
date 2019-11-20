import { loadClanInvitations, loadClanInvitation, sendClanInvitation, acceptClanInvitation, declineClanInvitation } from "./actions";
import {
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
  DECLINE_CLAN_INVITATION_FAIL
} from "./types";

describe("invitations", () => {
  it("should create an action to get user invitations", () => {
    const type = "user";
    const id = "0";

    const expectedType = [LOAD_CLAN_INVITATIONS, LOAD_CLAN_INVITATIONS_SUCCESS, LOAD_CLAN_INVITATIONS_FAIL];
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

    const expectedType = [LOAD_CLAN_INVITATIONS, LOAD_CLAN_INVITATIONS_SUCCESS, LOAD_CLAN_INVITATIONS_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = `/clans/api/invitations?${type}Id=${id}`;

    const actionCreator = loadClanInvitations(type, id);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get a specific invitation", () => {
    const invitationId = "10";

    const expectedType = [LOAD_CLAN_INVITATION, LOAD_CLAN_INVITATION_SUCCESS, LOAD_CLAN_INVITATION_FAIL];
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

    const expectedType = [SEND_CLAN_INVITATION, SEND_CLAN_INVITATION_SUCCESS, SEND_CLAN_INVITATION_FAIL];
    const expectedMethod = "POST";
    const expectedUrl = "/clans/api/invitations";

    const actionCreator = sendClanInvitation(clanId, userId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
    expect(actionCreator.payload.request.data).toEqual({ clanId: clanId, userId: userId });
  });

  it("should create an action to accept a invitation", () => {
    const invitationId = "10";

    const expectedType = [ACCEPT_CLAN_INVITATION, ACCEPT_CLAN_INVITATION_SUCCESS, ACCEPT_CLAN_INVITATION_FAIL];
    const expectedMethod = "POST";
    const expectedUrl = `/clans/api/invitations/${invitationId}`;

    const actionCreator = acceptClanInvitation(invitationId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to decline a invitation", () => {
    const invitationId = "10";

    const expectedType = [DECLINE_CLAN_INVITATION, DECLINE_CLAN_INVITATION_SUCCESS, DECLINE_CLAN_INVITATION_FAIL];
    const expectedMethod = "DELETE";
    const expectedUrl = `/clans/api/invitations/${invitationId}`;

    const actionCreator = declineClanInvitation(invitationId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});

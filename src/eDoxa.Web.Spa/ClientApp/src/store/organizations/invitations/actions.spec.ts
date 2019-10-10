import { loadInvitations, loadInvitation, addInvitation, acceptInvitation, declineInvitation } from "./actions";
import {
  LOAD_INVITATIONS,
  LOAD_INVITATIONS_SUCCESS,
  LOAD_INVITATIONS_FAIL,
  LOAD_INVITATION,
  LOAD_INVITATION_SUCCESS,
  LOAD_INVITATION_FAIL,
  ADD_INVITATION,
  ADD_INVITATION_SUCCESS,
  ADD_INVITATION_FAIL,
  ACCEPT_INVITATION,
  ACCEPT_INVITATION_SUCCESS,
  ACCEPT_INVITATION_FAIL,
  DECLINE_INVITATION,
  DECLINE_INVITATION_SUCCESS,
  DECLINE_INVITATION_FAIL
} from "./types";

describe("invitations", () => {
  it("should create an action to get user invitations", () => {
    const type = "user";
    const id = "0";

    const expectedType = [LOAD_INVITATIONS, LOAD_INVITATIONS_SUCCESS, LOAD_INVITATIONS_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = `/organizations/clans/api/invitations?${type}Id=${id}`;

    const actionCreator = loadInvitations(type, id);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get clan invitations", () => {
    const type = "clan";
    const id = "100";

    const expectedType = [LOAD_INVITATIONS, LOAD_INVITATIONS_SUCCESS, LOAD_INVITATIONS_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = `/organizations/clans/api/invitations?${type}Id=${id}`;

    const actionCreator = loadInvitations(type, id);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get a specific invitation", () => {
    const invitationId = "10";

    const expectedType = [LOAD_INVITATION, LOAD_INVITATION_SUCCESS, LOAD_INVITATION_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = `/organizations/clans/api/invitations/${invitationId}`;

    const actionCreator = loadInvitation(invitationId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to add a invitation", () => {
    const clanId = "10";
    const userId = "10";

    const expectedType = [ADD_INVITATION, ADD_INVITATION_SUCCESS, ADD_INVITATION_FAIL];
    const expectedMethod = "POST";
    const expectedUrl = "/organizations/clans/api/invitations";

    const actionCreator = addInvitation(clanId, userId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
    expect(actionCreator.payload.request.data).toEqual({ clanId: clanId, userId: userId });
  });

  it("should create an action to accept a invitation", () => {
    const invitationId = "10";

    const expectedType = [ACCEPT_INVITATION, ACCEPT_INVITATION_SUCCESS, ACCEPT_INVITATION_FAIL];
    const expectedMethod = "POST";
    const expectedUrl = `/organizations/clans/api/invitations/${invitationId}`;

    const actionCreator = acceptInvitation(invitationId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to decline a invitation", () => {
    const invitationId = "10";

    const expectedType = [DECLINE_INVITATION, DECLINE_INVITATION_SUCCESS, DECLINE_INVITATION_FAIL];
    const expectedMethod = "DELETE";
    const expectedUrl = `/organizations/clans/api/invitations/${invitationId}`;

    const actionCreator = declineInvitation(invitationId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});

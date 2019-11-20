import { downloadClanLogo, uploadClanLogo } from "./actions";
import { DOWNLOAD_CLAN_LOGO, DOWNLOAD_CLAN_LOGO_SUCCESS, DOWNLOAD_CLAN_LOGO_FAIL, UPLOAD_CLAN_LOGO, UPLOAD_CLAN_LOGO_SUCCESS, UPLOAD_CLAN_LOGO_FAIL } from "./types";

describe("clans", () => {
  it("should create an action to get a specific clan photo", () => {
    const clanId = "0";

    const expectedType = [DOWNLOAD_CLAN_LOGO, DOWNLOAD_CLAN_LOGO_SUCCESS, DOWNLOAD_CLAN_LOGO_FAIL];
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

    const expectedType = [UPLOAD_CLAN_LOGO, UPLOAD_CLAN_LOGO_SUCCESS, UPLOAD_CLAN_LOGO_FAIL];
    const expectedMethod = "POST";
    const expectedUrl = `/clans/api/clans/${clanId}/logo`;

    const actionCreator = uploadClanLogo(clanId, data);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
    expect(actionCreator.payload.request.data).toEqual(data);
  });
});

import {
  loadDoxaTags,
  loadDoxaTagHistory,
  changeDoxaTag,
  loadPersonalInfo,
  createPersonalInfo,
  updatePersonalInfo,
  loadAddressBook,
  addAddress,
  updateAddress,
  removeAddress,
  confirmEmail,
  forgotPassword,
  resetPassword,
  loadGames
} from "./actionCreators";

describe("identity actions", () => {
  it("should create an action to get user doxatag", () => {
    const expectedType = ["LOAD_DOXATAGS", "LOAD_DOXATAGS_SUCCESS", "LOAD_DOXATAGS_FAIL"];
    const expectedMethod = "GET";
    const expectedUrl = "/identity/api/doxatags";

    const actionCreator = loadDoxaTags();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get user doxatag history", () => {
    const expectedType = ["LOAD_DOXATAG_HISTORY", "LOAD_DOXATAG_HISTORY_SUCCESS", "LOAD_DOXATAG_HISTORY_FAIL"];
    const expectedMethod = "GET";
    const expectedUrl = "/identity/api/doxatag-history";

    const actionCreator = loadDoxaTagHistory();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to post user doxatag", () => {
    const expectedType = ["CHANGE_DOXATAG", "CHANGE_DOXATAG_SUCCESS", "CHANGE_DOXATAG_FAIL"];
    const expectedMethod = "POST";
    const expectedUrl = "/identity/api/doxatag-history";
    const expectedDoxaTag = "DoxaTag";

    const actionCreator = changeDoxaTag(expectedDoxaTag);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
    expect(actionCreator.payload.request.data).toEqual(expectedDoxaTag);
  });

  it("should create an action to get user personal info", () => {
    const expectedType = ["LOAD_PERSONAL_INFO", "LOAD_PERSONAL_INFO_SUCCESS", "LOAD_PERSONAL_INFO_FAIL"];
    const expectedMethod = "GET";
    const expectedUrl = "/identity/api/personal-info";

    const actionCreator = loadPersonalInfo();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to post user personal info", () => {
    const expectedType = ["CREATE_PERSONAL_INFO", "CREATE_PERSONAL_INFO_SUCCESS", "CREATE_PERSONAL_INFO_FAIL"];
    const expectedMethod = "POST";
    const expectedUrl = "/identity/api/personal-info";
    const expectedPersonalInfo = { firstName: "Bob", lastName: "Afrete" };

    const object = createPersonalInfo(expectedPersonalInfo);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
    expect(object.payload.request.data).toEqual(expectedPersonalInfo);
  });

  it("should create an action to put user doxatag", () => {
    const expectedType = ["UPDATE_PERSONAL_INFO", "UPDATE_PERSONAL_INFO_SUCCESS", "UPDATE_PERSONAL_INFO_FAIL"];
    const expectedMethod = "PUT";
    const expectedUrl = "/identity/api/personal-info";
    const expectedPersonalInfo = { firstName: "Bob", lastName: "Afrete" };

    const object = updatePersonalInfo(expectedPersonalInfo);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
    expect(object.payload.request.data).toEqual(expectedPersonalInfo);
  });

  it("should create an action to get user address book", () => {
    const expectedType = ["LOAD_ADDRESS_BOOK", "LOAD_ADDRESS_BOOK_SUCCESS", "LOAD_ADDRESS_BOOK_FAIL"];
    const expectedMethod = "GET";
    const expectedUrl = "/identity/api/address-book";

    const object = loadAddressBook();

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to post user address book", () => {
    const expectedType = ["ADD_ADDRESS", "ADD_ADDRESS_SUCCESS", "ADD_ADDRESS_FAIL"];
    const expectedMethod = "POST";
    const expectedUrl = "/identity/api/address-book";
    const expectedAddress = { country: "Canada", city: "Montreal" };

    const object = addAddress(expectedAddress);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
    expect(object.payload.request.data).toEqual(expectedAddress);
  });

  it("should create an action to put user address book", () => {
    const addressId = "1";

    const expectedType = ["UPDATE_ADDRESS", "UPDATE_ADDRESS_SUCCESS", "UPDATE_ADDRESS_FAIL"];
    const expectedMethod = "PUT";
    const expectedUrl = `/identity/api/address-book/${addressId}`;
    const expectedAddress = { country: "Canada", city: "Montreal" };

    const object = updateAddress(addressId, expectedAddress);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
    expect(object.payload.request.data).toEqual(expectedAddress);
  });

  it("should create an action to remove user address book", () => {
    const addressId = "1";

    const expectedType = ["REMOVE_ADDRESS", "REMOVE_ADDRESS_SUCCESS", "REMOVE_ADDRESS_FAIL"];
    const expectedMethod = "DELETE";
    const expectedUrl = `/identity/api/address-book/${addressId}`;

    const object = removeAddress(addressId);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to remove user address book", () => {
    const addressId = "1";
    const expectedType = ["REMOVE_ADDRESS", "REMOVE_ADDRESS_SUCCESS", "REMOVE_ADDRESS_FAIL"];
    const expectedMethod = "DELETE";
    const expectedUrl = `/identity/api/address-book/${addressId}`;

    const object = removeAddress(addressId);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get confirm email", () => {
    const userId = "1";
    const code = "0";
    const expectedType = ["CONFRIM_EMAIL", "CONFRIM_EMAIL_SUCCESS", "CONFRIM_EMAIL_FAIL"];
    const expectedMethod = "GET";
    const expectedUrl = `/identity/api/email/confirm?userId=${userId}&code=${code}`;

    const object = confirmEmail(userId, code);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to post user forgot password", () => {
    const expectedType = ["FORGOT_PASSWORD", "FORGOT_PASSWORD_SUCCESS", "FORGOT_PASSWORD_FAIL"];
    const expectedMethod = "POST";
    const expectedUrl = "/identity/api/password/forgot";
    const expectedEmail = { email: "gab@edoxa.gg" };

    const object = forgotPassword(expectedEmail);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
    expect(object.payload.request.data).toEqual(expectedEmail);
  });

  it("should create an action to post user reset password", () => {
    const expectedType = ["RESET_PASSWORD", "RESET_PASSWORD_SUCCESS", "RESET_PASSWORD_FAIL"];
    const expectedMethod = "POST";
    const expectedUrl = "/identity/api/password/reset";
    const expectedEmail = { email: "gab@edoxa.gg", password: "password" };

    const object = resetPassword(expectedEmail);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
    expect(object.payload.request.data).toEqual(expectedEmail);
  });

  it("should create an action to get user games id", () => {
    const expectedType = ["LOAD_GAMES", "LOAD_GAMES_SUCCESS", "LOAD_GAMES_FAIL"];
    const expectedMethod = "GET";
    const expectedUrl = "/identity/api/games";

    const object = loadGames();

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });
});

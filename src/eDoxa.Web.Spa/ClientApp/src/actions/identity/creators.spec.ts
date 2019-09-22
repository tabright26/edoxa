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
} from "./creators";
import actionTypes from "./index";

describe("identity actions", () => {
  it("should create an action to get user doxatag", () => {
    const expectedType = [actionTypes.LOAD_DOXATAGS, actionTypes.LOAD_DOXATAGS_SUCCESS, actionTypes.LOAD_DOXATAGS_FAIL];
    const expectedMethod = "get";
    const expectedUrl = "/identity/api/doxatags";

    const actionCreator = loadDoxaTags();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get user doxatag history", () => {
    const expectedType = [actionTypes.LOAD_DOXATAG_HISTORY, actionTypes.LOAD_DOXATAG_HISTORY_SUCCESS, actionTypes.LOAD_DOXATAG_HISTORY_FAIL];
    const expectedMethod = "get";
    const expectedUrl = "/identity/api/doxatag-history";

    const actionCreator = loadDoxaTagHistory();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to post user doxatag", () => {
    const expectedType = [actionTypes.CHANGE_DOXATAG, actionTypes.CHANGE_DOXATAG_SUCCESS, actionTypes.CHANGE_DOXATAG_FAIL];
    const expectedMethod = "post";
    const expectedUrl = "/identity/api/doxatag-history";
    const expectedDoxaTag = "DoxaTag";

    const actionCreator = changeDoxaTag(expectedDoxaTag);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
    expect(actionCreator.payload.request.data).toEqual(expectedDoxaTag);
  });

  it("should create an action to get user personal info", () => {
    const expectedType = [actionTypes.LOAD_PERSONAL_INFO, actionTypes.LOAD_PERSONAL_INFO_SUCCESS, actionTypes.LOAD_PERSONAL_INFO_FAIL];
    const expectedMethod = "get";
    const expectedUrl = "/identity/api/personal-info";

    const actionCreator = loadPersonalInfo();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to post user personal info", () => {
    const expectedType = [actionTypes.CREATE_PERSONAL_INFO, actionTypes.CREATE_PERSONAL_INFO_SUCCESS, actionTypes.CREATE_PERSONAL_INFO_FAIL];
    const expectedMethod = "post";
    const expectedUrl = "/identity/api/personal-info";
    const expectedPersonalInfo = { firstName: "Bob", lastName: "Afrete" };

    const object = createPersonalInfo(expectedPersonalInfo);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
    expect(object.payload.request.data).toEqual(expectedPersonalInfo);
  });

  it("should create an action to put user doxatag", () => {
    const expectedType = [actionTypes.UPDATE_PERSONAL_INFO, actionTypes.UPDATE_PERSONAL_INFO_SUCCESS, actionTypes.UPDATE_PERSONAL_INFO_FAIL];
    const expectedMethod = "put";
    const expectedUrl = "/identity/api/personal-info";
    const expectedPersonalInfo = { firstName: "Bob", lastName: "Afrete" };

    const object = updatePersonalInfo(expectedPersonalInfo);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
    expect(object.payload.request.data).toEqual(expectedPersonalInfo);
  });

  it("should create an action to get user address book", () => {
    const expectedType = [actionTypes.LOAD_ADDRESS_BOOK, actionTypes.LOAD_ADDRESS_BOOK_SUCCESS, actionTypes.LOAD_ADDRESS_BOOK_FAIL];
    const expectedMethod = "get";
    const expectedUrl = "/identity/api/address-book";

    const object = loadAddressBook();

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to post user address book", () => {
    const expectedType = [actionTypes.ADD_ADDRESS, actionTypes.ADD_ADDRESS_SUCCESS, actionTypes.ADD_ADDRESS_FAIL];
    const expectedMethod = "post";
    const expectedUrl = "/identity/api/address-book";
    const expectedAddress = { country: "Canada", city: "Montreal" };

    const object = addAddress(expectedAddress);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
    expect(object.payload.request.data).toEqual(expectedAddress);
  });

  it("should create an action to put user address book", () => {
    const addressId = 1;

    const expectedType = [actionTypes.UPDATE_ADDRESS, actionTypes.UPDATE_ADDRESS_SUCCESS, actionTypes.UPDATE_ADDRESS_FAIL];
    const expectedMethod = "put";
    const expectedUrl = `/identity/api/address-book/${addressId}`;
    const expectedAddress = { country: "Canada", city: "Montreal" };

    const object = updateAddress(addressId, expectedAddress);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
    expect(object.payload.request.data).toEqual(expectedAddress);
  });

  it("should create an action to remove user address book", () => {
    const addressId = 1;

    const expectedType = [actionTypes.REMOVE_ADDRESS, actionTypes.REMOVE_ADDRESS_SUCCESS, actionTypes.REMOVE_ADDRESS_FAIL];
    const expectedMethod = "delete";
    const expectedUrl = `/identity/api/address-book/${addressId}`;

    const object = removeAddress(addressId);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to remove user address book", () => {
    const addressId = 1;

    const expectedType = [actionTypes.REMOVE_ADDRESS, actionTypes.REMOVE_ADDRESS_SUCCESS, actionTypes.REMOVE_ADDRESS_FAIL];
    const expectedMethod = "delete";
    const expectedUrl = `/identity/api/address-book/${addressId}`;

    const object = removeAddress(addressId);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get confirm email", () => {
    const userId = 1;
    const code = 0;

    const expectedType = [actionTypes.CONFRIM_EMAIL, actionTypes.CONFRIM_EMAIL_SUCCESS, actionTypes.CONFRIM_EMAIL_FAIL];
    const expectedMethod = "get";
    const expectedUrl = `/identity/api/email/confirm?userId=${userId}&code=${code}`;

    const object = confirmEmail(userId, code);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to post user forgot password", () => {
    const expectedType = [actionTypes.FORGOT_PASSWORD, actionTypes.FORGOT_PASSWORD_SUCCESS, actionTypes.FORGOT_PASSWORD_FAIL];
    const expectedMethod = "post";
    const expectedUrl = "/identity/api/password/forgot";
    const expectedEmail = { email: "gab@edoxa.gg" };

    const object = forgotPassword(expectedEmail);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
    expect(object.payload.request.data).toEqual(expectedEmail);
  });

  it("should create an action to post user reset password", () => {
    const expectedType = [actionTypes.RESET_PASSWORD, actionTypes.RESET_PASSWORD_SUCCESS, actionTypes.RESET_PASSWORD_FAIL];
    const expectedMethod = "post";
    const expectedUrl = "/identity/api/password/reset";
    const expectedEmail = { email: "gab@edoxa.gg", password: "password" };

    const object = resetPassword(expectedEmail);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
    expect(object.payload.request.data).toEqual(expectedEmail);
  });

  it("should create an action to get user games id", () => {
    const expectedType = [actionTypes.LOAD_GAMES, actionTypes.LOAD_GAMES_SUCCESS, actionTypes.LOAD_GAMES_FAIL];
    const expectedMethod = "get";
    const expectedUrl = "/identity/api/games";

    const object = loadGames();

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });
});

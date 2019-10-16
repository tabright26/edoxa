import { loadAddressBook, addAddress, updateAddress, removeAddress } from "./actions";
import {
  LOAD_USER_ADDRESSBOOK,
  LOAD_USER_ADDRESSBOOK_SUCCESS,
  LOAD_USER_ADDRESSBOOK_FAIL,
  CREATE_USER_ADDRESS,
  CREATE_USER_ADDRESS_SUCCESS,
  CREATE_USER_ADDRESS_FAIL,
  DELETE_USER_ADDRESS,
  DELETE_USER_ADDRESS_SUCCESS,
  DELETE_USER_ADDRESS_FAIL,
  UPDATE_USER_ADDRESS,
  UPDATE_USER_ADDRESS_SUCCESS,
  UPDATE_USER_ADDRESS_FAIL
} from "./types";

describe("identity actions", () => {
  it("should create an action to get user address book", () => {
    const expectedType = [LOAD_USER_ADDRESSBOOK, LOAD_USER_ADDRESSBOOK_SUCCESS, LOAD_USER_ADDRESSBOOK_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = "/identity/api/address-book";

    const object = loadAddressBook();

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to post user address book", () => {
    const expectedType = [CREATE_USER_ADDRESS, CREATE_USER_ADDRESS_SUCCESS, CREATE_USER_ADDRESS_FAIL];
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

    const expectedType = [UPDATE_USER_ADDRESS, UPDATE_USER_ADDRESS_SUCCESS, UPDATE_USER_ADDRESS_FAIL];
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

    const expectedType = [DELETE_USER_ADDRESS, DELETE_USER_ADDRESS_SUCCESS, DELETE_USER_ADDRESS_FAIL];
    const expectedMethod = "DELETE";
    const expectedUrl = `/identity/api/address-book/${addressId}`;

    const object = removeAddress(addressId);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to remove user address book", () => {
    const addressId = "1";
    const expectedType = [DELETE_USER_ADDRESS, DELETE_USER_ADDRESS_SUCCESS, DELETE_USER_ADDRESS_FAIL];
    const expectedMethod = "DELETE";
    const expectedUrl = `/identity/api/address-book/${addressId}`;

    const object = removeAddress(addressId);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });
});

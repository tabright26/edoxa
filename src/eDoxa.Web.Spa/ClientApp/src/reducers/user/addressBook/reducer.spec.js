import { reducer, initialState } from "./reducer";
import * as types from "../../../actions/identityActions";

const addressId = "test_id";
const addressBook204Data = [];
const addressBook200Data = [{ id: "1" }, { id: "2" }, { id: "1" }];

const removeSuccessData = { addressId: "1" };
const removeExpectedState = initialState.filter(address => address.id !== addressId);

const axiosFailErrorData = { isAxiosError: false, response: { data: { errors: [{ id: "1" }, { id: "2" }] } } };

describe("user address book reducer", () => {
  it("should return the initial state", () => {
    expect(reducer(initialState, {})).toEqual(initialState);
  });

  it("should handle LOAD_ADDRESS_BOOK_SUCCESS 204", () => {
    expect(
      reducer(initialState, {
        type: types.LOAD_ADDRESS_BOOK_SUCCESS,
        payload: { status: 204, data: addressBook204Data }
      })
    ).toEqual(initialState);
  });

  it("should handle LOAD_ADDRESS_BOOK_SUCCESS 200", () => {
    expect(
      reducer(initialState, {
        type: types.LOAD_ADDRESS_BOOK_SUCCESS,
        payload: { status: 200, data: addressBook200Data }
      })
    ).toEqual(addressBook200Data);
  });

  it("should handle REMOVE_ADDRESS_SUCCESS", () => {
    expect(
      reducer(initialState, {
        type: types.REMOVE_ADDRESS_SUCCESS,
        payload: { data: removeSuccessData }
      })
    ).toEqual(removeExpectedState);
  });

  it("should handle ADD_ADDRESS_FAIL", () => {
    expect(
      reducer(initialState, {
        type: types.ADD_ADDRESS_FAIL,
        error: axiosFailErrorData
      })
    ).toEqual(initialState);
  });

  it("should handle UPDATE_ADDRESS_FAIL", () => {
    expect(
      reducer(initialState, {
        type: types.UPDATE_ADDRESS_FAIL,
        error: axiosFailErrorData
      })
    ).toEqual(initialState);
  });

  it("should handle REMOVE_ADDRESS_FAIL", () => {
    expect(
      reducer(initialState, {
        type: types.REMOVE_ADDRESS_FAIL,
        error: axiosFailErrorData
      })
    ).toEqual(initialState);
  });

  it("should handle ADD_ADDRESS_SUCCESS", () => {
    expect(
      reducer(initialState, {
        type: types.ADD_ADDRESS_SUCCESS
      })
    ).toEqual(initialState);
  });

  it("should handle UPDATE_ADDRESS_SUCCESS", () => {
    expect(
      reducer(initialState, {
        type: types.UPDATE_ADDRESS_SUCCESS
      })
    ).toEqual(initialState);
  });

  it("should handle LOAD_ADDRESS_BOOK_FAIL", () => {
    expect(
      reducer(initialState, {
        type: types.LOAD_ADDRESS_BOOK_FAIL
      })
    ).toEqual(initialState);
  });
});

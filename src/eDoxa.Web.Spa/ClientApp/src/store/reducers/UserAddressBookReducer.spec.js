import { reducer } from "./userAddressBookReducer";
import * as types from "../actions/identityActions";

describe("user address book reducer", () => {
  it("should return the initial state", () => {
    expect(reducer([], {})).toEqual([]);
  });

  it("should handle LOAD_ADDRESS_BOOK_SUCCESS 204", () => {
    expect(
      reducer([], {
        type: types.LOAD_ADDRESS_BOOK_SUCCESS,
        payload: { status: 204, data: "Run the tests" }
      })
    ).toEqual([]);
  });

  it("should handle LOAD_ADDRESS_BOOK_SUCCESS Empty", () => {
    expect(
      reducer([], {
        type: types.LOAD_ADDRESS_BOOK_SUCCESS,
        payload: { status: {}, data: "Run the tests" }
      })
    ).toEqual("Run the tests");
  });

  it("should handle ADD_ADDRESS_SUCCESS", () => {
    expect(
      reducer([], {
        type: types.ADD_ADDRESS_SUCCESS,
        payload: { data: "This is an error message" }
      })
    ).toEqual([]);
  });

  it("should handle UPDATE_ADDRESS_SUCCESS", () => {
    expect(
      reducer([], {
        type: types.UPDATE_ADDRESS_SUCCESS,
        payload: { data: "This is an error message" }
      })
    ).toEqual([]);
  });

  it("should handle REMOVE_ADDRESS_SUCCESS", () => {
    expect(
      reducer([], {
        type: types.REMOVE_ADDRESS_SUCCESS,
        payload: { data: "This is an error message" }
      })
    ).toEqual([]);
  });

  it("should handle LOAD_ADDRESS_BOOK_FAIL", () => {
    expect(
      reducer([], {
        type: types.LOAD_ADDRESS_BOOK_FAIL,
        payload: { data: "This is an error message" }
      })
    ).toEqual([]);
  });
});

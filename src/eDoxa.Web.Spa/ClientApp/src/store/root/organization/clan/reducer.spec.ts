import { reducer, initialState } from "./reducer";
import {
  LOAD_CLANS_SUCCESS,
  LOAD_CLANS_FAIL,
  LOAD_CLAN_SUCCESS,
  LOAD_CLAN_FAIL,
  CREATE_CLAN_SUCCESS
} from "store/actions/clan/types";
import { AxiosError } from "axios";

const clans204Data = [];
const clans200Data = [
  {
    id: "0",
    name: "Clan1",
    summary: "This is a summary",
    ownerId: "0",
    members: [{ userId: "0", clanId: "0" }]
  },
  {
    id: "1",
    name: "Clan2",
    summary: "This is a summary",
    ownerId: "1",
    members: [{ userId: "1", clanId: "1" }]
  },
  {
    id: "2",
    name: "Clan3",
    summary: "This is a summary",
    ownerId: "2",
    members: [{ userId: "2", clanId: "2" }]
  }
];

const clan200Data = {
  id: "0",
  name: "Clan1",
  summary: "This is a summary",
  ownerId: "0",
  members: [{ userId: "0", clanId: "0" }]
};

describe("candidatures reducer", () => {
  it("should handle LOAD_CLANS_SUCCESS 204", () => {
    const action: any = {
      type: LOAD_CLANS_SUCCESS,
      payload: { status: 204, data: clans204Data }
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_CLANS_SUCCESS 200", () => {
    const action: any = {
      type: LOAD_CLANS_SUCCESS,
      payload: { status: 200, data: clans200Data }
    };
    const state = {
      data: clans200Data,
      error: null,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });

  it("should handle LOAD_CLANS_FAIL", () => {
    const error: AxiosError = {
      isAxiosError: true,
      config: {},
      name: "",
      message: ""
    };
    const action: any = {
      type: LOAD_CLANS_FAIL,
      error
    };
    const state = {
      data: initialState.data,
      error,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });

  it("should handle LOAD_CLAN_SUCCESS", () => {
    const action: any = {
      type: LOAD_CLAN_SUCCESS,
      payload: { data: clan200Data }
    };
    const state = {
      data: [...initialState.data, clan200Data],
      error: null,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });

  it("should handle LOAD_CLAN_FAIL", () => {
    const error: AxiosError = {
      isAxiosError: true,
      config: {},
      name: "",
      message: ""
    };
    const action: any = {
      type: LOAD_CLAN_FAIL,
      error
    };
    const state = {
      data: initialState.data,
      error,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });

  it("should handle ADD_CLAN_SUCCESS", () => {
    const action: any = {
      type: CREATE_CLAN_SUCCESS
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });
});

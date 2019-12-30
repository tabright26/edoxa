import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Clans from "./Clans";
import { ClansState } from "store/root/organization/clan/types";

it("renders without crashing", () => {
  //Arrange
  const clans: ClansState = {
    data: [
      {
        name: "Clan 1",
        id: "1",
        logo: "1231wqeqwe1212e12e12e12e1",
        members: [],
        ownerId: "123"
      },
      {
        name: "Clan 2",
        id: "2",
        logo: "1231wqeqwe1212e12e12e12e1",
        members: [],
        ownerId: "123456"
      },
      {
        name: "Clan 3",
        id: "3",
        logo: "1231wqeqwe1212e12e12e12e1",
        members: [],
        ownerId: "789"
      }
    ],
    loading: false,
    error: null
  };

  const store: any = {
    getState: () => {
      return {
        oidc: {
          user: {
            profile: {
              sub: "123123123"
            }
          }
        },
        root: {
          organization: {
            clan: clans
          }
        }
      };
    },
    dispatch: action => {},
    subscribe: () => {}
  };

  //Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <Clans />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});

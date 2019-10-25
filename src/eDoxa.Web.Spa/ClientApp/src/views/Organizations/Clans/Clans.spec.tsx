import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Clans from "./Clans";
import { ClansState } from "store/root/organizations/clans/types";
import { DoxatagsState } from "store/root/doxaTags/types";

it("renders without crashing", () => {
  //Arrange
  const clans: ClansState = {
    data: [
      {
        name: "Clan 1",
        clanId: "1",
        members: [{ userId: "123123123" }]
      },
      {
        name: "Clan 2",
        clanId: "2",
        members: [{ userId: "321321321" }]
      },
      {
        name: "Clan 3",
        clanId: "3",
        members: [{ userId: "123456789" }]
      }
    ],
    loading: false,
    error: null
  };

  const doxatags: DoxatagsState = {
    data: [
      {
        name: "User 1",
        userId: "123123123",
        code: 1111,
        timestamp: 1111111
      },
      {
        name: "User 2",
        userId: "321321321",
        code: 2222,
        timestamp: 1111111
      },
      {
        name: "User 3",
        userId: "123456789",
        code: 3333,
        timestamp: 1111111
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
          organizations: {
            clans
          },
          doxatags
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

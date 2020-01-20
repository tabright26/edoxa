import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Details from ".";
import { ClansState } from "store/root/organization/clan/types";

it("renders without crashing", () => {
  //Arrange
  const clan: ClansState = {
    data: [
      {
        id: "1",
        logo: "qqwenwqj123n12ijni1n2ieb12ie1i2ubeiu12bei1u2bei",
        ownerId: "123214",
        name: "Clan 1",
        members: []
      },
      {
        id: "2",
        logo: "qqwenwqj123n12ijni1n2ieb12ie1i2ubeiu12bei1u2bei",
        ownerId: "123214",
        name: "Clan 2",
        members: []
      },
      {
        id: "3",
        logo: "qqwenwqj123n12ijni1n2ieb12ie1i2ubeiu12bei1u2bei",
        ownerId: "123214",
        name: "Clan 3",
        members: []
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
            clan
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
        <Details
          match={{ params: "1", isExact: false, path: "", url: "" }}
          history={null}
          location={null}
        />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});

import { connect } from "react-redux";
import { linkGameCredential } from "store/root/user/games/actions";
import {
  LINK_GAME_CREDENTIAL_SUCCESS,
  LINK_GAME_CREDENTIAL_FAIL,
  LinkGameCredentialAction
} from "store/root/user/games/types";
import Link from "./Validate";
import { Game } from "types";

interface OwnProps {
  game: Game;
  setAuthFactor: (data: any) => any;
}

const mapDispatchToProps = (dispatch: any, ownProps: OwnProps) => {
  return {
    linkGameCredential: () =>
      dispatch(linkGameCredential(ownProps.game)).then(
        (action: LinkGameCredentialAction) => {
          switch (action.type) {
            case LINK_GAME_CREDENTIAL_SUCCESS: {
              ownProps.setAuthFactor(null);
              break;
            }
            case LINK_GAME_CREDENTIAL_FAIL: {
              ownProps.setAuthFactor(null);
              break;
            }
          }
        }
      )
  };
};

export default connect(null, mapDispatchToProps)(Link);

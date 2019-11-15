import { connect } from "react-redux";
import { validateGameAccountAuthentication } from "store/root/user/game/authentication/actions";
import {
  VALIDATE_GAME_AUTHENTICATION_SUCCESS,
  VALIDATE_GAME_AUTHENTICATION_FAIL,
  GameAuthenticationActions
} from "store/root/user/game/authentication/types";
import Link from "./Validate";
import { Game } from "types";

interface OwnProps {
  game: Game;
  setAuthFactor: (data: any) => any;
}

const mapDispatchToProps = (dispatch: any, ownProps: OwnProps) => {
  return {
    validateGameAccountAuthentication: () =>
      dispatch(validateGameAccountAuthentication(ownProps.game)).then(
        (action: GameAuthenticationActions) => {
          switch (action.type) {
            case VALIDATE_GAME_AUTHENTICATION_SUCCESS: {
              ownProps.setAuthFactor(null);
              break;
            }
            case VALIDATE_GAME_AUTHENTICATION_FAIL: {
              ownProps.setAuthFactor(null);
              break;
            }
          }
        }
      )
  };
};

export default connect(null, mapDispatchToProps)(Link);

import { connect } from "react-redux";
import { validateGameAuthentication } from "store/root/user/game/authentication/actions";
import { loadGames } from "store/root/game/actions";
import {
  VALIDATE_GAME_AUTHENTICATION_SUCCESS,
  VALIDATE_GAME_AUTHENTICATION_FAIL,
  GameAuthenticationActions
} from "store/root/user/game/authentication/types";
import Link from "./Validate";
import { Game } from "types";
import { toastr } from "react-redux-toastr";

interface OwnProps {
  game: Game;
  handleCancel: () => any;
  setAuthenticationFactor: (data: any) => any;
}

const mapDispatchToProps = (dispatch: any, ownProps: OwnProps) => {
  return {
    validateGameAuthentication: () =>
      dispatch(validateGameAuthentication(ownProps.game)).then(
        (action: GameAuthenticationActions) => {
          switch (action.type) {
            case VALIDATE_GAME_AUTHENTICATION_SUCCESS: {
              return dispatch(loadGames()).then(() => ownProps.handleCancel());
            }
            case VALIDATE_GAME_AUTHENTICATION_FAIL: {
              toastr.error("Error", "Validating game authentication failed.");
              ownProps.setAuthenticationFactor(null);
              break;
            }
          }
          return Promise.resolve(action);
        }
      )
  };
};

export default connect(null, mapDispatchToProps)(Link);

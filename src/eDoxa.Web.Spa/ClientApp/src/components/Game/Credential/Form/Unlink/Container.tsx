import { connect } from "react-redux";
import { unlinkGameCredential } from "store/actions/identity/actions";
import { loadGames } from "store/actions/game/actions";
import {
  UNLINK_GAME_CREDENTIAL_SUCCESS,
  UNLINK_GAME_CREDENTIAL_FAIL,
  UnlinkGameCredentialAction
} from "store/actions/identity/types";
import Unlink from "./Unlink";
import { throwSubmissionError } from "utils/form/types";
import { Game } from "types";
import authorize from "utils/oidc/AuthorizeService";

interface OwnProps {
  game: Game;
}

const mapDispatchToProps = (dispatch: any, ownProps: OwnProps) => {
  return {
    unlinkGameCredential: () =>
      dispatch(unlinkGameCredential(ownProps.game)).then(
        (action: UnlinkGameCredentialAction) => {
          switch (action.type) {
            case UNLINK_GAME_CREDENTIAL_SUCCESS: {
              return dispatch(loadGames()).then(() => {
                console.log(window.location.pathname);
                return authorize
                  .getUser()
                  .then(user => console.log(user))
                  .then(() =>
                    authorize
                      .signIn({
                        returnUrl: window.location.pathname
                      })
                      .then(() => authorize.getUser().then(x => console.log(x)))
                  );
              });
            }
            case UNLINK_GAME_CREDENTIAL_FAIL: {
              throwSubmissionError(action.error);
              break;
            }
          }
          return Promise.resolve(action);
        }
      )
  };
};

export default connect(null, mapDispatchToProps)(Unlink);

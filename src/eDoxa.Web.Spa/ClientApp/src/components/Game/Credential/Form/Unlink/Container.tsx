import { connect } from "react-redux";
import { unlinkGameCredential } from "store/root/user/game/credential/actions";
import { loadGames } from "store/root/game/actions";
import {
  UNLINK_GAME_CREDENTIAL_SUCCESS,
  UNLINK_GAME_CREDENTIAL_FAIL,
  UnlinkGameCredentialAction
} from "store/root/user/game/credential/types";
import Unlink from "./Unlink";
import { throwSubmissionError } from "utils/form/types";
import { Game } from "types";

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
              return dispatch(loadGames());
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

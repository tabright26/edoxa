import { connect } from "react-redux";
import { unlinkGameCredential } from "store/root/user/games/actions";
import { UNLINK_GAME_CREDENTIAL_FAIL, UnlinkGameCredentialAction } from "store/root/user/games/types";
import Unlink from "./Unlink";
import { throwSubmissionError } from "utils/form/types";
import { Game } from "types";

interface OwnProps {
  game: Game;
}

const mapDispatchToProps = (dispatch: any, ownProps: OwnProps) => {
  return {
    unlinkGameCredential: () =>
      dispatch(unlinkGameCredential(ownProps.game)).then((action: UnlinkGameCredentialAction) => {
        switch (action.type) {
          case UNLINK_GAME_CREDENTIAL_FAIL: {
            throwSubmissionError(action.error);
            break;
          }
        }
      })
  };
};

export default connect(
  null,
  mapDispatchToProps
)(Unlink);

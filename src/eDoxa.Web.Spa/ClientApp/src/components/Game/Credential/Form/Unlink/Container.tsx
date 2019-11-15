import { connect } from "react-redux";
import { unlinkGameAccountCredential } from "store/root/user/game/credential/actions";
import { UNLINK_GAME_CREDENTIAL_FAIL, UnlinkGameCredentialAction } from "store/root/user/game/credential/types";
import Unlink from "./Unlink";
import { throwSubmissionError } from "utils/form/types";
import { Game } from "types";

interface OwnProps {
  game: Game;
}

const mapDispatchToProps = (dispatch: any, ownProps: OwnProps) => {
  return {
    unlinkGameCredential: () =>
      dispatch(unlinkGameAccountCredential(ownProps.game)).then((action: UnlinkGameCredentialAction) => {
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

import { connect } from "react-redux";
import { RootState } from "store/types";
import {
  loadUserDoxatagHistory,
  updateUserDoxatag
} from "store/root/user/doxatagHistory/actions";
import {
  UserDoxatagHistoryActions,
  UPDATE_USER_DOXATAG_FAIL
} from "store/root/user/doxatagHistory/types";
import Update from "./Update";
import { throwSubmissionError } from "utils/form/types";

const mapStateToProps = (state: RootState) => {
  const { data } = state.root.user.doxatagHistory;
  const doxatag =
    data
      .slice()
      .sort((left: any, right: any) =>
        left.timestamp < right.timestamp ? 1 : -1
      )[0] || null;
  return {
    initialValues: doxatag
  };
};

const mapDispatchToProps = (dispatch: any) => {
  return {
    updateUserDoxatag: (data: any) =>
      dispatch(updateUserDoxatag(data)).then(
        (action: UserDoxatagHistoryActions) => {
          switch (action.type) {
            case UPDATE_USER_DOXATAG_FAIL: {
              throwSubmissionError(action.error);
              break;
            }
            default: {
              dispatch(loadUserDoxatagHistory());
              break;
            }
          }
        }
      )
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(Update);

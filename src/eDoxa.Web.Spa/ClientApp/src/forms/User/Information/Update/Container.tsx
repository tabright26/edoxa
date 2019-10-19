import { connect } from "react-redux";
import { RootState } from "store/types";
import Update from "./Update";
import { loadUserInformations, updateUserInformations } from "store/root/user/informations/actions";
import { UserInformationsActions, UPDATE_USER_INFORMATIONS_FAIL } from "store/root/user/informations/types";
import { throwSubmissionError } from "utils/form/types";

const mapStateToProps = (state: RootState) => {
  const { data } = state.root.user.informations;
  return {
    initialValues: data
  };
};

const mapDispatchToProps = (dispatch: any) => {
  return {
    updateUserInformations: (data: any) =>
      dispatch(updateUserInformations(data)).then((action: UserInformationsActions) => {
        switch (action.type) {
          case UPDATE_USER_INFORMATIONS_FAIL: {
            throwSubmissionError(action.error);
            break;
          }
          default: {
            dispatch(loadUserInformations());
            break;
          }
        }
      })
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Update);

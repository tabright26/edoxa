import React, { useState, FunctionComponent, useEffect } from "react";
import Moment from "react-moment";
import { faEdit } from "@fortawesome/free-solid-svg-icons";
import { Card, CardHeader, CardBody } from "reactstrap";
import UserProfileForm from "components/User/Profile/Form";
import { compose } from "recompose";
import Button from "components/Shared/Button";
import Loading from "components/Shared/Loading";
import { connect } from "react-redux";
import { RootState } from "store/types";
import { loadUserProfile } from "store/actions/identity";

const Profile: FunctionComponent<any> = ({
  className,
  profile: { data, loading, error },
  loadUserProfile
}) => {
  useEffect((): void => {
    if (data === null) {
      loadUserProfile();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  const [buttonDisabled, setbuttonDisabled] = useState(false);
  return (
    <Card className={`card-accent-primary ${className}`}>
      <CardHeader className="d-flex">
        <strong className="text-uppercase my-auto">INFORMATIONS</strong>
        <Button.Link
          className="p-0 ml-auto my-auto"
          icon={faEdit}
          disabled={buttonDisabled || !data}
          onClick={() => setbuttonDisabled(true)}
        >
          UPDATE
        </Button.Link>
      </CardHeader>
      <CardBody>
        {loading ? (
          <Loading />
        ) : !data ? (
          <UserProfileForm.Create
            handleCancel={() => setbuttonDisabled(false)}
          />
        ) : !buttonDisabled ? (
          <dl className="row mb-0">
            <dd className="col-sm-3 text-muted">Name</dd>
            <dd className="col-sm-9">
              {data.firstName} {data.lastName}
            </dd>
            <dd className="col-sm-3 text-muted">Date of birth</dd>
            <dd className="col-sm-9">
              {data.dob && (
                <Moment
                  date={[data.dob.year, data.dob.month - 1, data.dob.day]}
                  format="ll"
                />
              )}
            </dd>
            <dd className="col-sm-3 text-muted mb-0">Gender</dd>
            <dd className="col-sm-9 mb-0">{data.gender}</dd>
          </dl>
        ) : (
          <UserProfileForm.Update
            handleCancel={() => setbuttonDisabled(false)}
          />
        )}
      </CardBody>
    </Card>
  );
};

const mapStateToProps = (state: RootState) => {
  return {
    profile: state.root.user.profile
  };
};

const mapDispatchToProps = (dispatch: any) => {
  return {
    loadUserProfile: () => dispatch(loadUserProfile())
  };
};

const enhance = compose<any, any>(connect(mapStateToProps, mapDispatchToProps));

export default enhance(Profile);

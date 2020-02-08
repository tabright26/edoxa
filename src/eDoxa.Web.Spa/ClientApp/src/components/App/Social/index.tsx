import React, { FunctionComponent } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faFacebook,
  faTwitter,
  faDiscord,
  faLinkedin,
  faInstagram
} from "@fortawesome/free-brands-svg-icons";

interface Props {
  className?: string;
}

export const Social: FunctionComponent<Props> = ({ className = null }) => (
  <ul className={`m-0 p-0 ${className}`}>
    <li className="d-inline mx-1">
      <a href={process.env.REACT_APP_FACEBOOK_URL}>
        <FontAwesomeIcon icon={faFacebook} />
      </a>
    </li>
    <li className="d-inline mx-1">
      <a href={process.env.REACT_APP_TWITTER_URL}>
        <FontAwesomeIcon icon={faTwitter} />
      </a>
    </li>
    <li className="d-inline mx-1">
      <a href={process.env.REACT_APP_DISCORD_URL}>
        <FontAwesomeIcon icon={faDiscord} />
      </a>
    </li>
    <li className="d-inline mx-1">
      <a href={process.env.REACT_APP_LINKEDIN_URL}>
        <FontAwesomeIcon icon={faLinkedin} />
      </a>
    </li>
    <li className="d-inline mx-1">
      <a href={process.env.REACT_APP_INSTAGRAM_URL}>
        <FontAwesomeIcon icon={faInstagram} />
      </a>
    </li>
  </ul>
);

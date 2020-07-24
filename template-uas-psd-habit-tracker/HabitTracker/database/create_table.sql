CREATE TABLE "user"(
	id UUID PRIMARY KEY,
	name VARCHAR(100) NOT NULL
);


CREATE TABLE "habit"(
	id UUID PRIMARY KEY,
	name VARCHAR(100) NOT NULL,
	user_id UUID NOT NULL,
	created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	FOREIGN KEY(user_id) REFERENCES "user"(id)
);

CREATE TABLE "daysoff"(
	id UUID PRIMARY KEY,
	days_off text[] NOT NULL,
	habit_id UUID NOT NULL,
	FOREIGN KEY(habit_id) REFERENCES "habit"(id)
);

CREATE TABLE "log"(
	id UUID PRIMARY KEY,
	logs TIMESTAMP[] NOT NULL,
	log_count INT NOT NULL,
	longest_streak INT NOT NULL,
	current_streak INT NOT NULL,
	habit_id UUID NOT NULL,
	FOREIGN KEY(habit_id) REFERENCES "habit"(id)
);

CREATE TABLE "badge"(
	id UUID PRIMARY KEY,
	name VARCHAR(100) NOT NULL,
	description VARCHAR(100) NOT NULL,
	user_id UUID NOT NULL,
	created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	FOREIGN KEY(user_id) REFERENCES "user"(id)
);
